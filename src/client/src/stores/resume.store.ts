import { defineStore } from 'pinia';
import { useAppStore } from './app.store';

export interface ResumeCard {
  id: string;
  title: string;
  status: 'Completed' | 'Draft' | 'New';
  updated: string;
  strength: number;
  image: string;
}

export interface CvMetadata {
  userName: string;
  plan: string;
  activeCount: number;
}

export interface ResumeDetail {
  id: string | null;
  title: string;
  autoSavedAt: string;
  personal: {
    firstName: string;
    lastName: string;
    role: string;
    email: string;
    phone: string;
    location: string;
    github?: string;
    linkedin?: string;
  };
  summary: string;
  tech: {
    programming: string[];
    databases: string[];
  };
  experiences: Array<{
    title: string;
    company: string;
    period: string;
    bullets: string[];
  }>;
  education: Array<{
    school: string;
    degree: string;
    period: string;
    gpa?: string;
  }>;
  projects: Array<{
    name: string;
    description: string;
    link?: string;
    bullets: string[];
  }>;
  volunteer: Array<{
    title: string;
    org: string;
    period: string;
    bullets: string[];
  }>;
  certifications: Array<{
    title: string;
    link?: string;
  }>;
  skills: string[]; // legacy; kept for form compatibility
}

export const createEmptyResume = (): ResumeDetail => ({
  id: null,
  title: 'Yeni CV',
  autoSavedAt: 'az önce',
  personal: {
    firstName: '',
    lastName: '',
    role: '',
    email: '',
    phone: '',
    location: '',
    github: '',
    linkedin: '',
  },
  summary: '',
  tech: { programming: [], databases: [] },
  experiences: [],
  education: [],
  projects: [],
  volunteer: [],
  certifications: [],
  skills: [],
});

interface State {
  items: ResumeCard[];
  metadata: CvMetadata | null;
  current: ResumeDetail | null;
}

export const useResumeStore = defineStore('resumes', {
  state: (): State => ({
    items: [],
    metadata: null,
    current: null,
  }),
  actions: {
    async fetchMock() {
      const app = useAppStore();
      return app.runWithLoading(async () => {
        // simulate backend metadata/cards
        await new Promise((resolve) => setTimeout(resolve, 200));
        this.metadata = {
          userName: 'Alex',
          plan: 'Pro Plan',
          activeCount: 3,
        };
        this.items = [
          {
            id: '1',
            title: 'Senior Frontend Dev',
            status: 'Completed',
            updated: '2 hours ago',
            strength: 100,
            image:
              'https://lh3.googleusercontent.com/aida-public/AB6AXuB0cFmVuA8k2D9nICsfSRGjOpiVai0hoWRa0i2nKNqHx_Sjubjop2KW1fWjNeiLYBkgysTm8-cGtKr1LZM3uUq0ebkD-iDndGRyRTR6PfzMznQTQoEcIVmiFjsdRoT5sXTwnlaCawLGmTIRfYfQJdYGV96E-QBKZsCILsxvxeHXQ4bRi5MEd3lfpeLPwfUHnCYF30-UhqjLAQHndrTbSurBwvLMrwrs2P05aiQ6nWC0R6pvNe9cTN55sbco0WizVce0AoMGFLHMMg',
          },
          {
            id: '2',
            title: 'UX Designer Portfolio',
            status: 'Draft',
            updated: 'yesterday',
            strength: 65,
            image:
              'https://lh3.googleusercontent.com/aida-public/AB6AXuDQsIUBxJD8kbIT1N8cS16ZjgZXSvXYNUvkISZWyCuDq98WIZrooii2L2tKDGzlG8utYt4_uJ4vjFgodQcYDx8Gbs8aBHb3yJorFmodw5UF3kxuRn8TfwOybC2EK7aWBJQYGSV6KAMNTjdmMDSI6K92aunA3tZx--kzz6WFORkV0ZoXOG3jVSviLrCK-xLMXFLXE3oota6LhxwtcLcJTK-Vly33ir7D59po6AW6ab3DAw-EEbvQ6UrHuZ2e8_J92L5pBNLwQjvYHw',
          },
          {
            id: '3',
            title: 'Freelance General',
            status: 'New',
            updated: '1 week ago',
            strength: 15,
            image:
              'https://lh3.googleusercontent.com/aida-public/AB6AXuAxBaq_K4Yd3-NauViRAkukA4t8lF7hUUzGieohWLDLxYvztQAS3UQ7LQeG1vLebCZtz0a1SdIsMtw_nAh1sWPJ8uwtVqL2yFjgyjwCFUcpWpHljqwjow7wMUv423dcVYMHHa6QE1Wwo_thu_xqATfeV7LlWD7puBIGiRYQTfBh6c3isGFneVzXqCA-v5ZYQL5Pr2O5kW4PwP18cjKaNTElr5vHOwuvuMzwrz4uGMVrlVQFQ0nAaWx3YkTDkSOClvcR-lIMMJaZwg',
          },
        ];
        return { metadata: this.metadata, items: this.items };
      });
    },
    async fetchDetail(id?: string) {
      const app = useAppStore();
      return app.runWithLoading(async () => {
        await new Promise((resolve) => setTimeout(resolve, 200));
        if (!id) {
          this.current = createEmptyResume();
        } else {
          this.current = {
            id,
            title: 'ALI OSMAN DEMİRKOLLU',
            autoSavedAt: '2 dk önce',
            personal: {
              firstName: 'ALI OSMAN',
              lastName: 'DEMİRKOLLU',
              role: 'Bilgisayar ve Öğretim Teknolojileri Eğitimi Öğrencisi',
              email: 'demirkolluossmanali@gmail.com',
              phone: '(533) 406-9864',
              location: 'Başakşehir, İstanbul',
              github: 'Github',
              linkedin: 'Linkedin',
            },
            summary:
              "Bilgisayar ve Öğretim Teknolojileri Eğitimi öğrencisi olarak teknolojiye olan tutkum ve problem çözme becerilerimle güçlü bir temel oluşturuyorum. Python, ReactJS, C#, ve Java gibi programlama dillerinde edindiğim orta seviyede bilgi ve SQL, SQLite, DBeaver, Git, Firebase gibi veritabanı ve bulut teknolojilerindeki tecrübemle projeler geliştirme konusunda kendime güveniyorum.\n\nEğitim hayatım süresince, yazılım geliştirme ve sistem yönetimi konularında gerçek dünya deneyimi kazandım. Yıldız Teknik Üniversitesi'ndeki akademik çalışmalarım ve DenizBank'taki staj deneyimim, teknik bilgi ve analitik becerilerimi pekiştirdi. Ayrıca, gönüllü çalışmalarımda sosyal medya ve tasarım konularında da deneyim kazanarak, farklı alanlarda yetkinliklerimi arttırdım.\n\nGelecekte, yazılım geliştirme ve teknolojik yeniliklerin öncüsü olmak istiyorum. Hedefim, teknolojiye dair derinlemesine bilgi sahibi olmak ve bu bilgiyi yenilikçi projelerde kullanarak gerçek dünyadaki problemleri çözmek. Hem bireysel hem de takım çalışması gerektiren projelerde kendimi geliştirerek, teknoloji alanında etkili ve yaratıcı çözümler üretmeye devam edeceğim.",
            tech: {
              programming: ['Python (Orta Derece)', 'ReactJS (Orta Derece)', 'C# (Orta Derece)', 'Java (Orta Derece)'],
              databases: ['SQL', 'SQLite', 'DBeaver', 'Git', 'Firebase'],
            },
            experiences: [
              {
                title: 'Asistan Öğrenci',
                company: 'Yıldız Teknik Üniversitesi',
                period: 'Kas 2022 – Mar 2023',
                bullets: [
                  'Akademisyenler ile takım çalışması yaparak çeşitli araştırma projeleri ve akademik görevleri yerine getirdim.',
                  'Bilgisayar laboratuvarlarının denetlenmesi, ilgili yazılımların yüklenmesi ve sınav süreçlerinde teknik destek sağladım.',
                  'Bölümdeki öğrencilerle aktif iletişim kurarak, yönlendirme ve danışmanlık hizmetleri sundum.',
                ],
              },
              {
                title: 'Stajyer',
                company: 'DenizBank',
                period: 'Ara 2021 – Oca 2022',
                bullets: [
                  "DenizBank'ın işleyiş tarzını ve bankanın üst yönetimiyle video konferanslar aracılığıyla yapılan görüşmeleri deneyimledim.",
                  'Bankacılık sektörünün işleyişini, alt birimlerin yapılandırılmasını ve bu birimlerin iş süreçlerini öğrendim.',
                  'Bölümler arasındaki farkları ve bağlantı noktalarını sektörde pratik deneyimle gözlemledim.',
                ],
              },
            ],
            education: [
              {
                school: 'Yıldız Teknik Üniversitesi (3. sınıf)',
                degree: 'Bilgisayar ve Öğretim Teknolojileri Eğitimi',
                period: '2020 - Devam Ediyor',
                gpa: 'GPA 3.17',
              },
              {
                school: 'Howard Community College, MD',
                degree: 'ESL – English as a Second Language',
                period: '2023 - 2024',
              },
            ],
            projects: [
              {
                name: 'Mail Otomasyon Sistemi SMTP',
                description: 'Bilgileri ve Etkinlikleri Mail Yolu ile İletme',
                bullets: [
                  'Bu projede, Python dilinin QtDesigner aracını kullanarak platform fark etmeksizin kulübe kayıt olanlara bilgileri ve etkinlikleri mail yolu ile ileten bir form oluşturdum.',
                  'Oluşturmuş olduğum bu program sayesinde, üyelerimizin bilgileri ve etkinlik duyuruları otomatik olarak e-posta yoluyla gönderildi.',
                ],
              },
              {
                name: 'Hava Durumu Uygulaması',
                description: 'Bir Konum veya Zip Kodu Üzerinden Havanın Durumunu Öğrenme',
                bullets: [
                  'Bu projede sadece istemci (client) bölümü bulunmaktadır.',
                  'Projenin önemli kısmı, bileşen mimarisi haricinde React teknolojisinin derinliklerine inerek React Hook, Actions, Reducer, Redux gibi teknolojileri kullanarak projeyi senkronluktan kurtarmaktır.',
                ],
              },
              {
                name: 'Istanbul Potansiyel Deprem Haritası',
                description: 'Veri Setlerini Kullanarak Harita Üzerinde Gösterme',
                bullets: [
                  'Bu projede, İstanbul için olası bir deprem senaryosunun harita üzerinde gösterimi yapıldı.',
                  'Verilen veriler ışığında, haritadaki riskli ve en az riskli bölgeler renklerle işaretlendi. Daha sonra bu butona tıklandığında, veri setinin içeriğini kullanarak depremdeki olasılıkları gösteren bir sekme açılması sağlandı.',
                ],
              },
            ],
            volunteer: [
              {
                title: 'Sosyal Medya ve Tasarım Ekip Üyesi',
                org: 'MİNT Öğrenci Kulübü',
                period: '2021-2023',
                bullets: [
                  'Medya biriminde, Adobe Creative Studio programları (Illustrator, Photoshop ve InDesign) ve Canva ile posterler ve Instagram gönderileri tasarladım. Ayrıca, e-dergi için yazılar yazdım.',
                ],
              },
              {
                title: 'Sekreter',
                org: 'Eğitim ve Bilişim Teknolojileri Kulübü',
                period: '2022-2023',
                bullets: [
                  'Toplantıları koordine ettim, gerekli belgeleri ve formları hazırladım, üniversite (YTÜ Sağlık Kültür ve Spor Daire Başkanlığı) ve öğrenci kulübü arasındaki iletişimi sağladım.',
                ],
              },
            ],
            certifications: [
              { title: 'Crash Course on Python – Coursera Google' },
              { title: 'An Introduction to Interactive Programming in Python (Part 1) – Coursera Rice University' },
              { title: 'SQL(BASIC) – HackerRank' },
              { title: 'Sıfırdan İleri Seviye Python Programlama – BTK Akademi' },
              { title: 'Google Project Management Basic – Coursera Google' },
              { title: 'Yemeksepeti: Crack the CX (Customer Experience Team Member)' },
            ],
            skills: [],
          };
        }
        return this.current;
      });
    },
  },
});
