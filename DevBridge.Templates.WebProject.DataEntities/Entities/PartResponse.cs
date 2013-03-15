namespace DevBridge.Templates.WebProject.DataEntities.Entities
{
    public class PartResponse : EntityBase<PartResponse>
    {
        public virtual MultipartUpload MultipartUpload { get; set; }
        public virtual int PartNumber { get; set; }
        public virtual string ETag { get; set; }
    }
}