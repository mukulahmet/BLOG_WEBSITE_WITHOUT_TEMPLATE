using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication1.Models.Entities;

namespace WebApplication1.Models.EntityConfiguration
{
    public class Article_CFG : IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.HasKey(x => x.ArticleId);
            builder.HasOne(x => x.User).WithMany(x => x.Articles).HasForeignKey(x => x.UserId);

            builder.HasData(
                new Article {ArticleId=1,Title="Sinemanın Önemi",Content= "Sinema, toplumsal bağları güçlendirir, kültürel ve duygusal bağlar kurar. İnsanların hayal gücünü besler, farklı bakış açıları sunar ve toplumsal konulara dikkat çeker. Eğlencenin yanı sıra, sinema sanatı sayesinde insanlar, farklı dünyalar ve yaşamları keşfederek empati geliştirme fırsatı bulur.", UserId=1 },

                  new Article { ArticleId = 2, Title = "Bilimin Günümüzdeki Yeri", Content = "Günümüzde bilim, teknolojinin ve yaşam kalitesinin temel taşıdır. Sağlık, iletişim, enerji ve çevre gibi alanlarda devrim niteliğinde ilerlemeler sağlar. Bilimsel araştırmalar, hastalıkların tedavisinde yenilikler sunar, iklim değişikliğiyle mücadelede kritik bilgiler sağlar ve günlük yaşamımızı daha verimli ve sürdürülebilir kılar.", UserId = 1 },

                   new Article { ArticleId = 3, Title = "İsatnbulda Gezilecek Yerler", Content = "İstanbul, tarihi ve kültürel zenginlikleriyle büyüleyici bir şehirdir. Ayasofya, Topkapı Sarayı ve Sultanahmet Camii gibi tarihi yapılar, Boğaz manzarası ve Kapalıçarşı’nın renkli atmosferi görülmeye değerdir. Galata Kulesi'nden panoramik şehir manzarası izlenebilir, İstiklal Caddesi boyunca keyifli bir yürüyüş yapılabilir.", UserId = 1 },

                    new Article { ArticleId = 4, Title = "Psikolojik Hastalıklar", Content = "Psikolojik hastalıklar, bireyin zihinsel sağlığını etkileyen bozukluklardır. Depresyon, anksiyete, bipolar bozukluk ve şizofreni gibi hastalıklar, duygusal ve davranışsal değişikliklere yol açar. Erken tanı ve tedavi, yaşam kalitesini artırmada kritiktir. Terapi, ilaç tedavisi ve destek grupları, iyileşme sürecinde önemli rol oynar.", UserId = 1 },

                     new Article { ArticleId = 5, Title = "Sinemanın Önemi", Content = "Eğitim sorunları, kalitesiz öğretim, yetersiz altyapı ve eşitsiz erişim gibi konuları içerir. Özellikle kırsal bölgelerde, maddi imkansızlıklar ve öğretmen eksikliği yaygındır. Eğitimde fırsat eşitsizliği, sosyal ve ekonomik kalkınmayı engeller. Etkili çözümler için devlet desteği, modern teknolojiler ve öğretmenlerin sürekli eğitimi önemlidir.", UserId = 1 }
                );
        }
    }
}


//, ArticleCategories = new List<ArticleCategory>() { new ArticleCategory() { ArticleCategoryId = 5, ArticleId = 5, CategoryId = 4 } }