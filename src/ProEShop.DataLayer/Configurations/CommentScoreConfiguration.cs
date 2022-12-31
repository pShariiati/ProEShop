using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProEShop.Entities;

namespace ProEShop.DataLayer.Configurations;

public class CommentScoreConfiguration : IEntityTypeConfiguration<CommentScore>
{
    public void Configure(EntityTypeBuilder<CommentScore> builder)
    {
        builder.HasKey(x => new { x.UserId, x.ProductCommentId });

        builder.HasOne(x => x.User)
            .WithMany(x => x.CommentsScores)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
