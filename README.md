# TodoApp API

Bu proje, .NET 9.0 kullanılarak geliştirilmiş bir Todo uygulaması API'sidir. JWT tabanlı kimlik doğrulama, Entity Framework Core, Identity ve SQL Server kullanılmaktadır.

## Gereksinimler

- .NET 9.0 SDK
- SQL Server (yerel veya Docker)
- Bir IDE (Visual Studio, Visual Studio Code, JetBrains Rider vb.)
- Docker ve Docker Compose (opsiyonel)

## Yerel Kurulum

### 1. Projeyi Klonlama

```bash
git clone https://github.com/eyupfurkantuylu/todobackend.git
cd todobackend
```

### 2. Veritabanı Kurulumu

#### SQL Server'ı Docker ile Çalıştırma (Opsiyonel)

Eğer yerel bir SQL Server kurulumunuz yoksa, Docker kullanarak SQL Server'ı çalıştırabilirsiniz:

```bash
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=Password123@" -p 1446:1433 --name sqlserver --restart always -d mcr.microsoft.com/mssql/server:2022-latest
```

### 3. Bağlantı Ayarları

`TodoApp.API/appsettings.json` dosyasındaki bağlantı dizesini kendi ortamınıza göre düzenleyin:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost,1446;Database=todoapp;User Id=sa;Password=Password123@;TrustServerCertificate=True;"
}
```

- `Server`: SQL Server'ınızın adresi ve portu
- `Database`: Veritabanı adı
- `User Id`: SQL Server kullanıcı adı
- `Password`: SQL Server şifresi
- `TrustServerCertificate`: SSL sertifikası doğrulamasını atlamak için (geliştirme ortamında kullanılabilir)

### 4. JWT Ayarları

`TodoApp.API/appsettings.json` dosyasındaki JWT ayarlarını güvenlik için değiştirmeniz önerilir:

```json
"JwtSettings": {
  "SecretKey": "buraya-guclu-ve-uzun-bir-gizli-anahtar-yazin",
  "Issuer": "TodoApp",
  "Audience": "TodoApp",
  "ExpirationInMinutes": 60,
  "RefreshTokenExpirationInDays": 7
}
```

> **Önemli**: Üretim ortamında güçlü ve uzun bir SecretKey kullanın. Yukarıdaki örnek anahtar sadece geliştirme amaçlıdır.

### 5. Projeyi Derleme ve Çalıştırma

```bash
cd TodoApp.API
dotnet restore
dotnet build
dotnet run
```

Uygulama varsayılan olarak https://localhost:5001 ve http://localhost:5000 adreslerinde çalışacaktır.

### 6. Swagger UI

Uygulama çalıştığında, API belgelerine ve test arayüzüne aşağıdaki URL üzerinden erişebilirsiniz:

```
https://localhost:5001/swagger
```

## Docker Compose ile Çalıştırma

Projeyi ve SQL Server'ı tek bir komutla çalıştırmak için Docker Compose kullanabilirsiniz:

```bash
docker-compose up -d
```

Bu komut:

1. SQL Server veritabanını çalıştırır
2. API projesini derler ve çalıştırır
3. Gerekli bağlantı ayarlarını otomatik olarak yapılandırır

API'ye http://localhost:8080 adresinden erişebilirsiniz.

Servisleri durdurmak için:

```bash
docker-compose down
```

Veritabanı verilerini silmek için:

```bash
docker-compose down -v
```

## Veritabanı Migrasyonları

Veritabanı şemasını oluşturmak veya güncellemek için Entity Framework Core migrasyonlarını kullanabilirsiniz:

```bash
cd TodoApp.API
dotnet ef migrations add InitialCreate
dotnet ef database update
```

## Deployment (Canlıya Alma)

### Azure App Service Deployment

1. Azure Portal'da bir App Service ve SQL Database oluşturun.

2. Bağlantı dizesini Azure App Service'in yapılandırma ayarlarında güncelleyin:

   - `DefaultConnection`: Azure SQL Database bağlantı dizesi

3. JWT ayarlarını Azure App Service'in yapılandırma ayarlarında güncelleyin:

   - `JwtSettings:SecretKey`: Güçlü bir gizli anahtar
   - `JwtSettings:Issuer`: Uygulamanızın adı veya URL'si
   - `JwtSettings:Audience`: Uygulamanızın adı veya URL'si
   - `JwtSettings:ExpirationInMinutes`: Token geçerlilik süresi (dakika)
   - `JwtSettings:RefreshTokenExpirationInDays`: Yenileme token'ı geçerlilik süresi (gün)

4. Visual Studio veya Azure CLI kullanarak uygulamayı deploy edin:

   ```bash
   dotnet publish -c Release
   az webapp deployment source config-zip --resource-group <resource-group> --name <app-name> --src bin/Release/net9.0/publish.zip
   ```

### Docker ile Deployment

1. Dockerfile ve docker-compose.yml dosyaları projenin kök dizininde bulunmaktadır.

2. Docker imajını oluşturun ve çalıştırın:

```bash
docker build -t todoapp-api .
docker run -d -p 8080:80 --name todoapp-api todoapp-api
```

3. Bağlantı dizesi ve JWT ayarlarını Docker çalıştırma komutunda ortam değişkenleri olarak belirtin:

```bash
docker run -d -p 8080:80 \
  -e "ConnectionStrings:DefaultConnection=Server=db-server;Database=todoapp;User Id=sa;Password=YourPassword;TrustServerCertificate=True;" \
  -e "JwtSettings:SecretKey=your-strong-secret-key" \
  -e "JwtSettings:Issuer=TodoApp" \
  -e "JwtSettings:Audience=TodoApp" \
  -e "JwtSettings:ExpirationInMinutes=60" \
  -e "JwtSettings:RefreshTokenExpirationInDays=7" \
  --name todoapp-api todoapp-api
```

## API Kullanımı

### Kimlik Doğrulama

#### Kullanıcı Kaydı

```
POST /api/auth/register
```

Örnek istek:

```json
{
  "userName": "kullanici",
  "email": "kullanici@example.com",
  "password": "Sifre123!"
}
```

#### Giriş Yapma

```
POST /api/auth/login
```

Örnek istek:

```json
{
  "email": "kullanici@example.com",
  "password": "Sifre123!"
}
```

Başarılı yanıt:

```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "refreshToken": "abc123def456...",
  "expiration": "2023-03-15T12:00:00Z"
}
```

#### Token Yenileme

```
POST /api/auth/refresh-token
```

Örnek istek:

```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "refreshToken": "abc123def456..."
}
```

### Todo İşlemleri

API'yi kullanmak için JWT token'ını Authorization header'ında belirtmeniz gerekir:

```
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...
```

## Lisans

Bu proje [MIT Lisansı](LICENSE) altında lisanslanmıştır.
