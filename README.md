Jadoo Travel Booking App, kullanıcıların kolayca **tur rezervasyonu** yapabildiği,  
**yapay zekâ destekli gezi öneri sistemi (AI Destinations)** barındıran ve  
admin panelinden rezervasyonların yönetilebildiği **modern bir Full Stack .NET Core uygulamasıdır**.

---

## 🎯 Amaç

- Kullanıcıların **rezervasyon** işlemlerini hızlı ve güvenli şekilde tamamlayabilmesi  
- **Yapay zekâ entegrasyonu** ile kullanıcılara şehir ve ülke bazlı **gezi önerileri** sunmak  
- **Dil çeviri API’si** ile çok dilli içerik oluşturmak  
- **Admin panelinde rezervasyon takibi**, onaylama ve analiz işlemleri gerçekleştirmek  
- **Chart.js** ile rezervasyon verilerini grafiksel olarak analiz etmek  

---

## 🛠️ Kullanılan Teknolojiler

### 🔹 Backend
- ASP.NET Core 9.0 (C#)
- MongoDB (NoSQL Database)
- AutoMapper
- OpenAI API (AI Destination önerileri)
- Translation API (Çok dilli içerik)

### 🔹 Frontend
- Razor Pages
- Bootstrap 5
- Chart.js
- SweetAlert2
- Responsive Spike Admin Tema

---

## 📌 Öne Çıkan Özellikler

| Özellik | Açıklama |
|----------|-----------|
| 🧭 Rezervasyon Formu | Kullanıcı adı, e-posta, şehir, tarih ve not bilgileriyle rezervasyon |
| 🧩 AI Destinations | Yapay zekâ tabanlı şehir & ülke bazlı gezi önerileri |
| 🌐 Dil Desteği | Translation API ile otomatik çok dilli içerik |
| 🗃️ MongoDB | NoSQL tabanlı veri saklama |
| 📊 Dinamik Grafikler | Chart.js ile kişi sayısı & rezervasyon analizi |
| 🧾 SweetAlert2 | Başarılı rezervasyon sonrası kullanıcı bildirimi |
| 🧠 Katmanlı Mimari | Controller – Service – DTO – Entity yapısı |
| 🎨 Spike Admin Panel | Modern yönetim arayüzü |

---

## 🤖 AI DESTINATIONS ÖZELLİĞİ

Kullanıcı, formda şehir ve ülke bilgilerini girdikten sonra:
1. OpenAI API çağrısı ile **gezilecek yer önerileri** oluşturulur.  
2. Translation API sayesinde öneriler otomatik olarak Türkçeye çevrilir.  
3. Modal pencerede kullanıcıya dinamik olarak gösterilir.
   
<img width="937" height="466" alt="SS1" src="https://github.com/user-attachments/assets/b6d1d76a-3b16-43c8-a802-3b3969a97285" />
<img width="945" height="455" alt="SS2" src="https://github.com/user-attachments/assets/5ceb9fa3-cbc4-4f95-ab85-0003f4a6a073" />
<img width="947" height="457" alt="SS3" src="https://github.com/user-attachments/assets/9cd86efd-019a-4e74-9720-6ad03d059faa" />
<img width="947" height="473" alt="SS4" src="https://github.com/user-attachments/assets/ed4c8877-d134-4619-a484-671e32ff3d52" />
<img width="943" height="473" alt="ss5" src="https://github.com/user-attachments/assets/b3b757b8-26e1-416d-8070-13983b238ace" />
<img width="945" height="473" alt="SS6" src="https://github.com/user-attachments/assets/e17fb280-fbe4-4da8-88d2-38f49c334f82" />
<img width="944" height="474" alt="SS7" src="https://github.com/user-attachments/assets/657ef387-f33b-49f9-aca0-43bdaabcfb35" />
<img width="943" height="473" alt="SS8" src="https://github.com/user-attachments/assets/ebcb3d65-9a06-4d31-8850-511aa89f5b8c" />
<img width="942" height="475" alt="SS9" src="https://github.com/user-attachments/assets/01be4718-7068-40a8-9c00-fa660f36abfd" />
<img width="948" height="475" alt="SS10" src="https://github.com/user-attachments/assets/46f28955-86c2-4b30-b065-8cfcea89bb02" />
<img width="947" height="475" alt="SS11" src="https://github.com/user-attachments/assets/32e78fc4-b140-4036-88b4-3e7cadcb1378" />
<img width="944" height="475" alt="SS12" src="https://github.com/user-attachments/assets/9e728ac4-7305-49f9-9962-282198b9bc9b" />
<img width="942" height="472" alt="SS13" src="https://github.com/user-attachments/assets/fa52e111-a7bb-40e9-b716-84c3b08e0600" />
<img width="946" height="350" alt="SS14" src="https://github.com/user-attachments/assets/ffc99210-6939-4a72-812a-842a99b65b42" />
<img width="943" height="471" alt="SS15" src="https://github.com/user-attachments/assets/fe509e60-d757-47c5-a8dd-ce0eb320f420" />
<img width="944" height="331" alt="SS16" src="https://github.com/user-attachments/assets/d394550b-71bb-47a5-99d9-0293093b2dce" />

