# Todo List Uygulaması PRD

## 1. Proje Genel Bakış

### 1.1 Amaç

Bu proje, kullanıcıların kişisel todo listlerini yönetebilecekleri güvenli ve kullanıcı dostu bir web API'si geliştirmeyi amaçlamaktadır.

### 1.2 Teknik Stack

- Backend: .NET Web API
- Veritabanı: MSSQL
- Kimlik Doğrulama: IdentityServer
- ORM Araçları:
  - Dapper (performans kritik sorgular için)
  - Entity Framework Core (kompleks ilişkiler ve CRUD işlemleri için)
- API Dokümantasyonu: Swagger

## 2. Sistem Mimarisi

### 2.1 Katmanlı Mimari

- Presentation Layer (API)
- Business Layer (Service)
- Data Access Layer
- Domain Layer
- Infrastructure Layer

### 2.2 Veritabanı Şeması

#### Roles Tablosu

- Id (PK)
- Name
- NormalizedName
- ConcurrencyStamp

#### UserRoles Tablosu

- UserId (PK, FK)
- RoleId (PK, FK)

#### Users Tablosu

- Id (PK)
- Username
- Email
- PasswordHash
- CreatedAt
- UpdatedAt

#### TodoLists Tablosu

- Id (PK)
- UserId (FK)
- Title
- Description
- CreatedAt
- UpdatedAt

#### TodoItems Tablosu

- Id (PK)
- TodoListId (FK)
- Title
- Description
- DueDate
- IsCompleted
- Priority (Enum: Low, Medium, High)
- CreatedAt
- UpdatedAt

## 3. API Endpoints

### 3.1 Kimlik Doğrulama ve Rol Yönetimi Endpoints

```
POST /api/auth/register
POST /api/auth/login
POST /api/auth/refresh-token
POST /api/auth/logout

# Rol Yönetimi
GET    /api/roles                # Tüm rolleri listele (Sadece Admin)
POST   /api/roles                # Yeni rol oluştur (Sadece Admin)
DELETE /api/roles/{id}           # Rol sil (Sadece Admin)
POST   /api/users/{id}/roles     # Kullanıcıya rol ata (Sadece Admin)
DELETE /api/users/{id}/roles     # Kullanıcıdan rol kaldır (Sadece Admin)
GET    /api/users/{id}/roles     # Kullanıcının rollerini getir
```

### 3.2 TodoList Endpoints

```
GET    /api/todolists              # Tüm listeleri getir
GET    /api/todolists/{id}         # Tek bir liste getir
POST   /api/todolists              # Yeni liste oluştur
PUT    /api/todolists/{id}         # Liste güncelle
DELETE /api/todolists/{id}         # Liste sil
```

### 3.3 TodoItem Endpoints

```
GET    /api/todolists/{listId}/items           # Liste içindeki tüm itemları getir
GET    /api/todolists/{listId}/items/{id}      # Tek bir item getir
POST   /api/todolists/{listId}/items           # Yeni item oluştur
PUT    /api/todolists/{listId}/items/{id}      # Item güncelle
DELETE /api/todolists/{listId}/items/{id}      # Item sil
PATCH  /api/todolists/{listId}/items/{id}/complete  # Item'ı tamamlandı olarak işaretle
```

## 4. Güvenlik Gereksinimleri

### 4.1 Kimlik Doğrulama

- IdentityServer ile JWT tabanlı kimlik doğrulama
- Token yenileme mekanizması
- Güvenli şifre politikası
- Rate limiting
- Rol tabanlı yetkilendirme (RBAC)
  - Admin: Tüm yetkilere sahip
  - User: Kendi todo listlerine erişim

### 4.2 Yetkilendirme

- Kullanıcılar sadece kendi todo listlerine erişebilir
- HTTPS zorunluluğu
- Cross-Origin Resource Sharing (CORS) politikası
- Rol bazlı erişim kontrolleri
  - Admin rolü tüm işlemleri yapabilir
  - User rolü sadece kendi verilerine erişebilir

## 5. Performans Gereksinimleri

### 5.1 Veritabanı Optimizasyonu

- İlgili tablolarda indeksler
- Dapper ile performans kritik sorgular
- Entity Framework için lazy loading optimizasyonu

### 5.2 API Performansı

- Sayfalama (Pagination)
- Filtreleme
- Caching mekanizması
- Asenkron operasyonlar

## 6. Hata Yönetimi

### 6.1 HTTP Durum Kodları

- 200: Başarılı işlemler
- 201: Başarılı oluşturma
- 400: Geçersiz istek
- 401: Yetkisiz erişim
- 403: Yasaklı erişim
- 404: Bulunamadı
- 500: Sunucu hatası

### 6.2 Hata Mesajları

- Kullanıcı dostu hata mesajları
- Detaylı loglama
- Global exception handling

## 7. Dokümantasyon

### 7.1 API Dokümantasyonu

- Swagger UI ile interaktif API dokümantasyonu
- Her endpoint için örnek request/response
- Authentication bilgileri

### 7.2 Teknik Dokümantasyon

- Kurulum kılavuzu
- Veritabanı şema dokümantasyonu
- Deployment prosedürleri

## 8. Test Gereksinimleri

### 8.1 Birim Testleri

- Service katmanı testleri
- Repository katmanı testleri
- Controller testleri

### 8.2 Entegrasyon Testleri

- API endpoint testleri
- Veritabanı entegrasyon testleri
- Authentication testleri
