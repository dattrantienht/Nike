# :thong_sandal: Nike - Backend :athletic_shoe:
Đây là Solution Backend cho bài tập ASP.NET, xây dựng trên nền template [Clean Architecture](https://github.com/iayti/CleanArchitecture) tạo ra bởi [İlker Aytı](https://github.com/iayti), áp dụng hình mẫu thiết kế [CQS](https://en.wikipedia.org/wiki/Command%E2%80%93query_separation) và tuân theo những nguyên tắc của Kiến Trúc Sạch (Clean Architecture) :ok_hand: .

## :tv: Công nghệ sử dụng :iphone:
* ASP.NET Core 5
* [Entity Framework Core 5](https://docs.microsoft.com/en-us/ef/core/)
* [MediatR](https://github.com/jbogard/MediatR)
* [Mapster](https://github.com/MapsterMapper/Mapster)

### :hammer_and_wrench: Cấu hình Database :anchor:

Hãy chắc chắn là **DefaultConnection** chuỗi kết nối trong **appsettings.json** trỏ đúng đến một thực thể SQL Server. 

Khi bạn chạy ứng dụng lần đầu tiên cơ sở dữ liệu sẽ được tạo ra tự động (nếu cần thiết) và file migrations mới nhất sẽ được áp dụng.

### :dragon: Database Migrations :black_cat:

Mở cmd ở folder gốc của solution(chứa file Nike.sln) Chạy lần lượt 2 lệnh này:

```batch
  dotnet ef migrations add "CreateDb" --project src\Common\Nike.Infrastructure --startup-project src\Apps\Nike.Api --output-dir Persistence\Migrations
```

```batch
  dotnet ef database update --project src\Common\Nike.Infrastructure --startup-project src\Apps\Nike.Api
```

## Tổng Quan 

### Domain

Ở đây bao gồm tất cả các lớp thực thể, enums, exceptions, interfaces, types và các logic cụ thể cho tầng domain.

### Application

Tầng này bao gồm các logic cho ứng dụng. Nó phụ thuộc vào tầng domain, nhưng không có tầng nào hay project nào bị phụ thuộc vào nó. Tầng này định nghĩa các interface được triển khai bởi các lớp khác. ví dụ, nếu tầng application cần truy cập vào một service notification, một interface mới sẽ được thêm vào tầng application và được triển khai ở tầng infrastructure.

### Infrastructure

Tầng này bao gồm các lớp thực hiện việc truy cập vào các tài nguyên như file systems, web services, smtp, v..v... Những class này nên được dựa trên những interface định nghĩa ở tầng application.

### WebApi

Tầng này là ứng dụng web API dựa trên ASP.NET 5. Phụ thuộc vào cả hai tầng Application và Infrastructure, tuy nhiên, sự phụ thuộc vào Infrastructure chỉ hỗ trợ với dependency injection. Do đó, chỉ có *Startup.cs* được tham chiếu đến tầng Infrastructure.