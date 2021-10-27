# :thong_sandal: Nike - Backend :athletic_shoe:
Đây là Solution Backend cho bài tập ASP.NET, xây dựng trên nền template [Clean Architecture](https://github.com/iayti/CleanArchitecture) tạo ra bởi [İlker Aytı](https://github.com/iayti), áp dụng hình mẫu thiết kế [CQRS](https://docs.microsoft.com/en-us/azure/architecture/patterns/cqrs) và tuân theo những nguyên tắc của Kiến Trúc Sạch (Clean Architecture) :ok_hand: .

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

