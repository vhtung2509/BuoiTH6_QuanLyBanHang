using ClosedXML.Excel;
using QuanLyBanHang.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static QuanLyBanHang.Data.HoaDon;

namespace QuanLyBanHang.Forms
{
    public partial class frmHoaDon : Form
    {
        QLBHDbContext context = new QLBHDbContext();
        int id;
        public frmHoaDon()
        {
            InitializeComponent();
        }

        private void frmHoaDon_Load(object sender, EventArgs e)
        {
            dataGridView.AutoGenerateColumns = false;

            // BƯỚC 1: Gọi .ToList() ngay lập tức để ngắt chuỗi dịch SQL của Entity Framework.
            // Dữ liệu sẽ được kéo trực tiếp lên bộ nhớ của phần mềm.
            var dsHoaDon = context.HoaDon.ToList();

            // BƯỚC 2: Lúc này dữ liệu đã nằm trên RAM, C# sẽ tự tính toán mà không sợ lỗi Int16
            List<DanhSachHoaDon> hd = dsHoaDon.Select(r => new DanhSachHoaDon
            {
                ID = r.ID,
                NhanVienID = r.NhanVienID,

                // Dùng r.NhanVien?.HoVaTen đề phòng trường hợp NhanVien bị null gây sập app
                HoVaTenNhanVien = r.NhanVien?.HoVaTen,

                KhachHangID = r.KhachHangID,
                HoVaTenKhachHang = r.KhachHang?.HoVaTen,

                NgayLap = r.NgayLap,
                GhiChuHoaDon = r.GhiChuHoaDon,

                // Phép tính này giờ sẽ chạy bằng C# thông thường, hoàn toàn mượt mà
                TongTienHoaDon = r.HoaDon_ChiTiet.Sum(ct => ct.SoLuongBan * ct.DonGiaBan),
                XemChiTiet = "Xem chi tiết"
            }).ToList();

            dataGridView.DataSource = hd;
        }

        private void btnLapHoaDon_Click(object sender, EventArgs e)
        {
            using (frmHoaDon_ChiTiet chiTiet = new frmHoaDon_ChiTiet())
            {
                chiTiet.ShowDialog();
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            id = Convert.ToInt32(dataGridView.CurrentRow.Cells["ID"].Value.ToString());
            using (frmHoaDon_ChiTiet chiTiet = new frmHoaDon_ChiTiet(id))
            {
                chiTiet.ShowDialog();
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Xác nhận xóa hóa đơn" + "?", "Xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                id = Convert.ToInt32(dataGridView.CurrentRow.Cells["ID"].Value.ToString());
                HoaDon hd = context.HoaDon.Find(id);
                if (hd != null)
                {
                    context.HoaDon.Remove(hd);
                }
                context.SaveChanges();

                frmHoaDon_Load(sender, e);
            }
        }

        private void btnNhap_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Nhập dữ liệu từ tập tin Excel";
            openFileDialog.Filter = "Tập tin Excel|*.xls;*.xlsx";
            openFileDialog.Multiselect = false;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    DataTable table = new DataTable();
                    using (XLWorkbook workbook = new XLWorkbook(openFileDialog.FileName))
                    {
                        IXLWorksheet worksheet = workbook.Worksheet(1);
                        bool firstRow = true;
                        string readRange = "1:1";
                        foreach (IXLRow row in worksheet.RowsUsed())
                        {
                            if (firstRow)
                            {
                                readRange = string.Format("{0}:{1}", 1, row.LastCellUsed().Address.ColumnNumber);
                                foreach (IXLCell cell in row.Cells(readRange))
                                    table.Columns.Add(cell.Value.ToString());
                                firstRow = false;
                            }
                            else
                            {
                                table.Rows.Add();
                                int cellIndex = 0;
                                foreach (IXLCell cell in row.Cells(readRange))
                                {
                                    table.Rows[table.Rows.Count - 1][cellIndex] = cell.Value.ToString();
                                    cellIndex++;
                                }
                            }
                        }
                        if (table.Rows.Count > 0)
                        {
                            foreach (DataRow r in table.Rows)
                            {
                                HoaDon hd = new HoaDon();

                                // Ép kiểu an toàn cho Mã Nhân Viên
                                int nhanVienId = 0;
                                int.TryParse(r["NhanVienID"].ToString(), out nhanVienId);
                                hd.NhanVienID = nhanVienId;

                                // Ép kiểu an toàn cho Mã Khách Hàng
                                int khachHangId = 0;
                                int.TryParse(r["KhachHangID"].ToString(), out khachHangId);
                                hd.KhachHangID = khachHangId;

                                // Xử lý Ngày Lập (Nếu trống sẽ tự lấy ngày hiện tại)
                                DateTime ngayLap = DateTime.Now;
                                if (!string.IsNullOrWhiteSpace(r["NgayLap"].ToString()))
                                    DateTime.TryParse(r["NgayLap"].ToString(), out ngayLap);
                                hd.NgayLap = ngayLap;

                                hd.GhiChuHoaDon = r["GhiChuHoaDon"].ToString();

                                context.HoaDon.Add(hd);
                            }
                            context.SaveChanges();

                            MessageBox.Show("Đã nhập thành công " + table.Rows.Count + " hóa đơn.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            frmHoaDon_Load(sender, e);
                        }
                        if (firstRow)
                            MessageBox.Show("Tập tin Excel rỗng.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void btnXuat_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Xuất dữ liệu ra tập tin Excel";
            saveFileDialog.Filter = "Tập tin Excel|*.xls;*.xlsx";
            saveFileDialog.FileName = "HoaDon_" + DateTime.Now.ToShortDateString().Replace("/", "_") + ".xlsx";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    DataTable table = new DataTable();

                    table.Columns.AddRange(new DataColumn[6] {
                        new DataColumn("ID", typeof(int)),
                        new DataColumn("NhanVienID", typeof(int)),
                        new DataColumn("KhachHangID", typeof(int)),
                        new DataColumn("NgayLap", typeof(DateTime)),
                        new DataColumn("GhiChuHoaDon", typeof(string)),
                        new DataColumn("TongTienHoaDon", typeof(decimal))
                    });

                    // Lấy dữ liệu lên RAM tương tự lúc Load để tránh lỗi Int16 khi tính tổng
                    var danhSachHD = context.HoaDon.ToList();
                    if (danhSachHD != null)
                    {
                        foreach (var p in danhSachHD)
                        {
                            var tongTien = p.HoaDon_ChiTiet.Sum(ct => ct.SoLuongBan * ct.DonGiaBan);
                            table.Rows.Add(p.ID, p.NhanVienID, p.KhachHangID, p.NgayLap, p.GhiChuHoaDon, tongTien);
                        }
                    }

                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        var sheet = wb.Worksheets.Add(table, "HoaDon");
                        sheet.Columns().AdjustToContents();
                        wb.SaveAs(saveFileDialog.FileName);

                        MessageBox.Show("Đã xuất dữ liệu ra tập tin Excel thành công.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }
    }
}