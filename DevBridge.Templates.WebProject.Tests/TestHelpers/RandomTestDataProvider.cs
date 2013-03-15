using System;
using System.Text;
using DevBridge.Templates.WebProject.DataEntities.Entities;
using DevBridge.Templates.WebProject.DataEntities.Enums;

namespace DevBridge.Templates.WebProject.Tests.TestHelpers
{
    public class RandomTestDataProvider
    {    
        private readonly Random random = new Random();

        public string ProvideRandomString(int length)
        {
            var sb = new StringBuilder();
            while (sb.Length < length)
            {
                sb.Append(Guid.NewGuid().ToString().Replace("-", string.Empty));
            }
            return sb.ToString().Substring(0, length);
        }

        public int ProvideRandomNumber(int min, int max)
        {
            return random.Next(min, max);
        }

        public decimal ProvideRandomNumber(decimal min, decimal max, int scale)
        {
            return Math.Round((decimal)((double)min + (random.NextDouble() * ((double)max - (double)min))), scale);
        }

        public decimal ProvideRandomNumber(decimal min, decimal max)
        {
            return (decimal)((double)min + (random.NextDouble() * ((double)max - (double)min)));
        }

        public double ProvideRandomNumber(double min, double max)
        {
            return min + (random.NextDouble() * (max - min));
        }

        public DateTime ProvideRandomDateTime()
        {
            return new DateTime(ProvideRandomNumber(1990, 2019), ProvideRandomNumber(1, 12), ProvideRandomNumber(1, 29),
                                ProvideRandomNumber(0, 23), ProvideRandomNumber(0, 59), ProvideRandomNumber(0, 59));
        }

        public Customer CreateCustomer()
        {
            return new Customer
                       {
                           Name = ProvideRandomString(50),
                           Code = ProvideRandomString(10),
                           Type = (CustomerType)ProvideRandomNumber(1, 6),
                           CreatedOn = ProvideRandomDateTime()
                       };
        }

        public Agreement CreateAgreement(Customer customer = null)
        {
            return new Agreement
                       {
                           Customer = customer ?? CreateCustomer(),
                           Number = ProvideRandomString(20),
                           CreatedOn = ProvideRandomDateTime()
                       };
        }

        public MultipartUpload CreateMultipartUpload()
        {
            return new MultipartUpload
            {
                BucketName = ProvideRandomString(500),
                ContentLength = 500,
                DocumentId = 5,
                DocumentType = 5,
                Hash = ProvideRandomString(250),
                KeyName = ProvideRandomString(500),
                UploadId = ProvideRandomString(500)
            };
        }

        public PartResponse CreatePartResponse()
        {
            return new PartResponse
            {
                ETag = ProvideRandomString(250),
                MultipartUpload = CreateMultipartUpload(),
                PartNumber = 5
            };
        }
    }
}
