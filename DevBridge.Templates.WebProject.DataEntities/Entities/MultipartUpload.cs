using System.Collections.Generic;

namespace DevBridge.Templates.WebProject.DataEntities.Entities
{
    public class MultipartUpload : EntityBase<MultipartUpload>
    {
        public MultipartUpload()
        {
            PartResponses = new List<PartResponse>();
        }

        public virtual int DocumentId { get; set; }
        public virtual int DocumentType { get; set; }
        public virtual string KeyName { get; set; }
        public virtual decimal ContentLength { get; set; }
        public virtual string UploadId { get; set; }
        public virtual string BucketName { get; set; }
        public virtual string Hash { get; set; }
        public virtual IList<PartResponse> PartResponses { get; set; }
    }
}