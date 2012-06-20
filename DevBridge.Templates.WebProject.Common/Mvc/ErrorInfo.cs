namespace DevBridge.Templates.WebProject.Common.Mvc
{
	public class ErrorInfo
	{
		public string ErrorMessage { get; set; }
		public string PropertyName { get; set; }

		public ErrorInfo(string propertyName, string errorMessage)
		{
			PropertyName = propertyName;
			ErrorMessage = errorMessage;
		}
	}
}