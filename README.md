# Website Bán Đồ Chơi HGK Toys

Dự án Website bán đồ chơi **HGK Toys** được phát triển trên nền tảng **ASP.NET MVC (.NET Framework 4.7.2)** và **SQL Server**. Đây là bài tập lớn môn **Phát triển ứng dụng Web (PTUDW)**.

## 🌟 Tính năng chính
- **Trang chủ & Giới thiệu:** Trình bày các sản phẩm đồ chơi nổi bật, danh mục sản phẩm.
- **Quản lý danh mục & sản phẩm:** Phân loại đồ chơi (đồ chơi trẻ em, giáo dục, mô hình, điện tử, thông minh,...).
- **Giỏ hàng & Đặt hàng:** Cho phép khách hàng thêm sản phẩm vào giỏ hàng, cập nhật số lượng và đặt hàng trực tuyến.
- **Quản lý đơn hàng:** Theo dõi tình trạng giao nhận và thanh toán.
- **Phân quyền người dùng:** 
  - Khách hàng (Xem sản phẩm, mua hàng, đăng ký/đăng nhập).
  - Nhân viên/Quản trị viên (Quản lý sản phẩm, đơn hàng, khách hàng, nhân viên).

## 🛠️ Công nghệ sử dụng
- **Backend:** C# ASP.NET MVC 5, Entity Framework 6.
- **Database:** SQL Server (sử dụng ADO.NET / EF Database First).
- **Frontend:** HTML5, CSS3, Bootstrap, JavaScript, jQuery.

## 📂 Cấu trúc dự án chính
- `Project_64130980.sln`: File Solution của dự án.
- `Project_64130980.sql`: File script cơ sở dữ liệu SQL Server (chứa cấu trúc bảng, dữ liệu mẫu và các Stored Procedure tìm kiếm).
- `Project_64130980/`: Thư mục mã nguồn chính của ứng dụng ASP.NET MVC.
  - `Controllers/`: Điều hướng và xử lý logic ứng dụng.
  - `Models/`: Định nghĩa các thực thể và kết nối database (Entity Framework).
  - `Views/`: Giao diện người dùng (Razor View).
  - `Content/` & `Scripts/`: Thư mục chứa CSS, JS và các thư viện frontend.
  - `Images/`: Hình ảnh sản phẩm và giao diện.

## 🚀 Hướng dẫn cài đặt cục bộ
1. **Cơ sở dữ liệu:**
   - Mở SQL Server Management Studio (SSMS).
   - Chạy file script [Project_64130980.sql](Project_64130980.sql) để tạo database và chèn dữ liệu mẫu.
2. **Cấu hình kết nối:**
   - Mở file `Web.config` trong thư mục [Project_64130980](Project_64130980/Web.config).
   - Thay đổi thuộc tính `data source` trong `connectionString` thành tên server SQL của bạn (ví dụ: `localhost` hoặc tên máy tính của bạn).
3. **Chạy ứng dụng:**
   - Mở file [Project_64130980.sln](Project_64130980.sln) bằng Visual Studio.
   - Restore các gói NuGet nếu cần thiết.
   - Nhấn **F5** hoặc **Start** để khởi chạy ứng dụng trên trình duyệt web qua IIS Express.

---
*Dự án được thực hiện bởi sinh viên **Hoàng Gia Khánh - 64130980**.*
