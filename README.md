# Nike - Backend
đây là Solution Backend cho bài tập ASP.NET, xây dựng trên nền template [CleanArchitecture](https://github.com/iayti/CleanArchitecture) tạo ra bởi [İlker Aytı](https://github.com/iayti) tuân theo nhưng nguyên tắc của Kiến Trúc Sạch (Clean Architecture)

## Công nghệ sử dụng
* ASP.NET Core 5
* [Entity Framework Core 5](https://docs.microsoft.com/en-us/ef/core/)
* [MediatR](https://github.com/jbogard/MediatR)
* [Mapster](https://github.com/MapsterMapper/Mapster)
* [FluentValidation](https://fluentvalidation.net/)
* [NUnit](https://nunit.org/), [FluentAssertions](https://fluentassertions.com/), [Moq](https://github.com/moq) & [Respawn](https://github.com/jbogard/Respawn)
* [Elasticsearch](https://www.elastic.co/), [Serilog](https://serilog.net/), [Kibana](https://www.elastic.co/kibana)
* [Docker](https://www.docker.com/)

### Cấu hình Database

Hãy chắc chắn là **DefaultConnection** chuỗi kết nối trong **appsettings.json** trỏ đến một thực thể SQL Server đúng. 

Khi bạn chạy ứng dụng lần đầu tiên cơ sở dữ liệu sẽ được tạo ra tự động (nếu cần thiết) và file migrations mới nhất sẽ được áp dụng.

### Database Migrations

Mở cmd ở folder gốc của solution(chứa file Nike.sln) Chạy lần lượt 2 lệnh này:

 `dotnet ef migrations add "CreateDb" --project src\Common\Nike.Infrastructure --startup-project src\Apps\Nike.Api --output-dir Persistence\Migrations`

 `dotnet ef database update --project src\Common\Nike.Infrastructure --startup-project src\Apps\Nike.Api`
