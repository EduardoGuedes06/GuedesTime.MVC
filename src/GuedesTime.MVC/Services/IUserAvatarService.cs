namespace GuedesTime.MVC.Services
{
	public interface IUserAvatarService
	{
		(bool ok, string error, string contentType, string protectedBase64) ProtectFromDataUrl(string dataUrl);
		(bool ok, string error, byte[] bytes, string contentType) UnprotectToBytes(string protectedBase64, string contentType);
	}
}

