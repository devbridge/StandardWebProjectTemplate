using DevBridge.Templates.WebProject.DataEntities.Entities;

namespace DevBridge.Templates.WebProject.DataEntities.Mappings
{
    public class MultipartUploadMap : EntityMapBase<MultipartUpload>
    {
        public MultipartUploadMap()
        {
            Table("MultipartUploads");

            Id(f => f.Id).GeneratedBy.Identity();

            Map(f => f.DocumentId).Not.Nullable();
            Map(f => f.DocumentType).Not.Nullable();
            Map(f => f.KeyName).Length(500).Not.Nullable();
            Map(f => f.ContentLength).Not.Nullable();
            Map(f => f.UploadId).Length(500).Not.Nullable();
            Map(f => f.BucketName).Length(500).Not.Nullable();
            Map(f => f.Hash).Length(250).Not.Nullable();

            HasMany(f => f.PartResponses).Cascade.All().Inverse();
        }
    }
}