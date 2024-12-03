# ASP.NET CORE 8 ile Kuaför Otomasyonu

## Hedef
+ Kullanıcılar için üyelik sistemi bulunması.
+ Adminler tarafından kuaför tanımlamalarının yapılabilmesi ve kuaför hizmetlerinin eklenip düzenlenebilmesi.
+ Adminler tarafından kullanıcı işlemlerinin gerçekleştirilebilmesi.
+ Kuaför sahibi olan kişilerin ilgili kuaför hakkındaki bilgileri güncelleyebilmesi.
+ Kuaför sahibi kişilerin kuaföre çalışan ekleyebilmesi; bu çalışanların verdiği hizmetleri seçip bu hizmetlerin ücret, süre gibi özelliklerini çalışan bazlı seçebilmeleri; her çalışan için ayrı günlere ayrı mesailer tanımlayabilmesi.
+ Kuaför sahibi kişilerin ilgili kuaföre ait randevuları görüntüleyebilmesi ve yönetebilmesi.
+ Kuaför çalışanlarının sadece kendilerine ait randevuları görebilmesi ve düzenleyebilmesi.
+ Bütün kullanıcıların şahsi bilgilerini güncelleyebilmeleri.
+ Bütün kullanıcıların dilediği kuaförden randevu alabilmeleri.
+ Bütün kullanıcıların mevcut randevularının durumunlarını takip edebilmeleri.
+ API servisi üzerinden kullanıcıların akışta fotoğraf paylaşabilmeleri.
+ Yapay zeka entegrasyonu sayesinde kullanıcıların saç rengi, saç kesimi vb gibi önerilerden bir tanesini alabilmesi.

## Kullanılacak Teknolojiler
+ ASP.NET Core 8
+ MsSQL
+ JQUERY, Ajax
+ Bootstrap
+ Html, CSS, Js

## Yol Haritası
- ASP.NET Core MVC projesi oluşturulması.
- N katmanlı mimaride çalışabilmek için "Class Library"ler eklenerek katmanların oluşturulması.
- Katmanlarda bulunması gereken temel "Concretes, Abstracts" gibi klasörlerin oluşturulması.
- Projenin temel dosya yapısının oluşturulmasıyla veritabanı katmanında bağlantı işlemlerinin tamamlanması.
- IdentityUser ve IdentityRole üzerinden sınıfların türetilmesi ve Authentication işlemlerinin backend- frontend olarak tamamlanması.
- Projede "Panel" MVC alanının oluşturulması.
- Panel alanında gerçekleşecek admin işlemleri için modellerin oluşturulması ve servislerin yazılması. Sonrasında ilgili aksiyonlar için arayüz tasarımlarının yapılması.
- Panel alanında gerçekleşecek kuaför sahibi işlemleri için modellerin oluşturulması ve servislerin yazılması. Sonrasında ilgili aksiyonlar için arayüz tasarımlarının yapılması.
- Kullanıcı arayüzü için randevu modeli oluşturulması, ilgili servislerin yazılması ve arayüzde uygulanması, tasarımın tamamlanması.
- Authorization işlemlerinin tamamlanması.
- Kullanıcıların kuaföre ait anılarını paylaşabildiği bir arayüz tasarımı oluşturulması, ilgili modellerin ve servislerin oluşturulması ve RestAPI üzerinden hizmetin servis edilmesi.
- Kullanıcıların fotoğraflarını yükleyip farklı saç modelleri için öneri aldığı ya da farklı saç modellerini deneyebildiği bir yapay zeka entegrasyonu.

## Authors
- [@Mesmer-yl](https://www.github.com/Mesmer-yl)
- [@oktayesmer](https://www.github.com/oktayesmer)

