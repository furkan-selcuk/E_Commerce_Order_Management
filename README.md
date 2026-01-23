# E-Commerce Order Management System

Bu proje, **.NET 8** ve **Blazor** teknolojileri kullanılarak geliştirilmiş, modern ve ölçeklenebilir bir E-Ticaret Sipariş Yönetim Sistemidir. Clean Architecture (Temiz Mimari) prensiplerine sadık kalınarak, bakımı kolay ve geliştirilmeye açık bir yapı sunar.

## 🚀 Teknolojiler ve Mimari

Proje, endüstri standardı teknolojiler ve kütüphanelerle güçlendirilmiştir:

*   **Backend:** .NET 8, ASP.NET Core Web API
*   **Frontend:** Blazor (Server/Web App), **DevExpress Blazor Components** ile zenginleştirilmiş UI
*   **Veritabanı Erişim (ORM):** **Dapper** (Yüksek performanslı veri erişimi için)
*   **Veritabanı:** Microsoft SQL Server
*   **Mimari:** N-Katmanlı Mimari / Clean Architecture

## 📦 Temel Modüller

Uygulama aşağıdaki ana işlevsellikleri içerir:

*   **Stok Yönetimi (`Stok`):** Ürünlerin kayıt, güncelleme, silme ve detaylı listeleme işlemleri.
*   **Cari Hesaplar (`Cari`):** Müşteri ve tedarikçi takibi, bakiye yönetimi.
*   **Fatura İşlemleri (`Fatura`):** Satış ve alış faturalarının oluşturulması ve raporlanması.


## 📂 Proje Yapısı (Solution Structure)

Proje, yönetilebilirliği artırmak amacıyla katmanlara ayrılmıştır:

| Katman | Açıklama |
| :--- | :--- |
| **`ECommerce.Domain`** | Projenin çekirdeği. Veritabanı varlıklarını (Entities), Enumları ve temel arayüzleri barındırır. Başka hiçbir katmana bağımlılığı yoktur. |
| **`ECommerce.DataAccess`** | Veri erişim katmanı. `Dapper` kullanarak veritabanı ile iletişim kurar. Repository pattern uygulanmıştır. |
| **`ECommerce.Application`** | İş mantığı katmanı. Servisler, DTO'lar (Data Transfer Objects) ve validasyonlar burada bulunur. |
| **`ECommerce.WebApi`** | Frontend'e veri sağlayan RESTful API katmanı. Controller'lar burada tanımlıdır. |
| **`ECommerce.Blazor`** | Kullanıcı arayüzü. Blazor bileşenleri (Pages/Components) ve UI mantığı burada yer alır. |

## 🛠️ Kurulum ve Çalıştırma

Projenin yerel makinenizde çalışması için aşağıdaki adımları izleyebilirsiniz:

1.  **Gereksinimler:**
    *   .NET 8 SDK yüklü olmalıdır.
    *   SQL Server (LocalDB veya Express) çalışır durumda olmalıdır.

2.  **Veritabanı Ayarları:**
    *   `ECommerce.WebApi` projesi altındaki `appsettings.json` dosyasını açın.
    *   `ConnectionStrings` bölümündeki veritabanı bağlantı adresini kendi ortamınıza göre düzenleyin.

3.  **Projeyi Başlatma:**
    *   Visual Studio veya tercih ettiğiniz IDE ile `ECommerce.Sln` dosyasını açın.
    *   **Startup Projects** olarak hem `ECommerce.WebApi` (Backend) hem de `ECommerce.Blazor` (Frontend) projelerini seçin.
    *   Projeyi derleyin ve çalıştırın.

---

### 🚧 Karşılaşılan Zorluklar ve Çözümler

*   **Dapper:** İlk ciddi zorluk, Dapper kullanarak ilişkili tabloların kurulumu ve veri yönetimi oldu. Entity Framework gibi bir ORM'in aksine, Dapper'da join işlemleri, mapping'ler ve veri akışını manuel olarak yönetmek gerekiyor. Bu süreç başlangıçta zorlayıcı olsa da, veritabanı yapısı üzerinde tam kontrol sahibi olmamı sağladı. SQL sorgularını optimize etme, performans yönetimi ve veri akışını mikro seviyede kontrol etme konusunda derinlemesine bilgi kazandım.
*   **DevExpress:** Projenin ilk aşamalarında, Blazor'ın komponent tabanlı yapısından tam anlamıyla faydalanmak için tüm sayfaları modüler komponentlere böldüm. Ancak bir sayfada 2'den fazla DevExpress komponenti kullanıldığında, sayfa yükleme süreleri kabul edilemez seviyelere ulaştı. Araştırmalarım sonucunda, bu sorunun ücretsiz lisans kullanımından ve her komponent için yapılan lisans doğrulama sorgularından kaynaklandığını öğrendim. Sonuç olarak, başlangıçta kurduğum full-component mimarisini tek sayfa (page-based) yapısına dönüştürmek zorunda kaldım. Ancak şunu belirtmek isterim ki, komponent tabanlı mimariyi başarıyla tasarlayıp uyguladım ve bu yaklaşıma tamamen hakimim - performans kısıtlamaları nedeniyle farklı bir yapıya geçiş yapmak bir tercih değil, zorunluluktu.  
*  **Fatura oluşturma** Daha önce ilişkili tablolar kurmuş olsam da, fatura oluşturma süreci beni en çok zorlayan kısımlardan biriydi. Özellikle master-detail ilişkisindeki tabloların (Fatura - FaturaDetay) foreign key'lerini manuel olarak yönetmek, transaction kontrolü sağlamak ve veri tutarlılığını korumak dikkat gerektiren bir süreçti. Bu deneyim, veritabanı tasarımında referential integrity kavramını pratikte uygulamam ve hata yönetimi konusunda daha bilinçli kod yazmam konusunda değerli bir deneyim kazandırdı.

### 💡 Öğrenilen Yeni Teknolojiler ve Yöntemler

*   **DevExpress Grid:** DevExpress Grid bileşeninin gelişmiş özelliklerini derinlemesine öğrendim: server-side/client-side filtreleme, sıralama, sayfalama (pagination), custom column templates, inline editing ve export işlemleri. Özellikle büyük veri setlerinde performans optimizasyonu için sanal scrolling ve lazy loading tekniklerini uyguladım.
*   **Blazor:** Blazor'ın component lifecycle'ını (OnInitialized, OnParametersSet, OnAfterRender) detaylı şekilde öğrendim. Routing yapısı, NavigationManager kullanımı, state management, parameter passing ve cascade parameters gibi konularda pratik deneyim kazandım. Ayrıca Blazor Server ve Blazor WebAssembly arasındaki farkları ve hangi senaryoda hangisinin tercih edilmesi gerektiğini anladım.
*   **Dapper:** Entity Framework'ten farklı olarak Dapper'ın "micro-ORM" yaklaşımını benimsedim. Multi-mapping, dynamic querying, stored procedure çağrıları, bulk operations ve transaction management konularında derinleşme fırsatı buldum. Özellikle QueryAsync, QueryMultiple, Execute metodları farklı senaryolarda etkin kullanmayı öğrendim. Ham SQL yazma deneyimim arttı ve sorgu optimizasyonu konusunda daha bilinçli hale geldim.



## 🔗 Kaynaklar ve İletişim

*   **Proje Sahibi:** Furkan Selçuk
*   **Dxexpress:** https://www.devexpress.com/
