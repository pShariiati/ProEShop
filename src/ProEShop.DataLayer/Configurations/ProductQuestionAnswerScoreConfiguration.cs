using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProEShop.Entities;

namespace ProEShop.DataLayer.Configurations;

public class ProductQuestionAnswerScoreConfiguration : IEntityTypeConfiguration<ProductQuestionAnswerScore>
{
    public void Configure(EntityTypeBuilder<ProductQuestionAnswerScore> builder)
    {
        builder.HasKey(x => new { x.UserId, x.AnswerId });

        builder.HasOne(x => x.User)
            .WithMany(x => x.ProductsQuestionsAnswersScores)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
