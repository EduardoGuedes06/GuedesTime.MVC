using System.Text;
using Microsoft.AspNetCore.DataProtection;

namespace GuedesTime.MVC.Services
{
	public class UserAvatarService : IUserAvatarService
	{
		private const int MaxBytes = 1_000_000;
		private readonly IDataProtector _protector;

		public UserAvatarService(IDataProtectionProvider provider)
		{
			_protector = provider.CreateProtector("GuedesTime.MVC.UserAvatar.v1");
		}

		public (bool ok, string error, string contentType, string protectedBase64) ProtectFromDataUrl(string dataUrl)
		{
			if (string.IsNullOrWhiteSpace(dataUrl) || !dataUrl.StartsWith("data:image/", StringComparison.OrdinalIgnoreCase))
				return (false, "Imagem inválida.", string.Empty, string.Empty);

			var commaIdx = dataUrl.IndexOf(',');
			if (commaIdx < 0)
				return (false, "Imagem inválida.", string.Empty, string.Empty);

			var header = dataUrl.Substring(0, commaIdx);
			var base64 = dataUrl.Substring(commaIdx + 1);

			var contentType = header.Contains("image/png", StringComparison.OrdinalIgnoreCase) ? "image/png"
				: header.Contains("image/jpeg", StringComparison.OrdinalIgnoreCase) ? "image/jpeg"
				: header.Contains("image/jpg", StringComparison.OrdinalIgnoreCase) ? "image/jpeg"
				: string.Empty;

			if (string.IsNullOrWhiteSpace(contentType))
				return (false, "Formato não suportado (use PNG ou JPG).", string.Empty, string.Empty);

			byte[] bytes;
			try { bytes = Convert.FromBase64String(base64); }
			catch { return (false, "Imagem inválida.", string.Empty, string.Empty); }

			if (bytes.Length == 0 || bytes.Length > MaxBytes)
				return (false, "Imagem muito grande (máx. 1MB).", string.Empty, string.Empty);

			var protectedBytes = _protector.Protect(bytes);
			return (true, string.Empty, contentType, Convert.ToBase64String(protectedBytes));
		}

		public (bool ok, string error, byte[] bytes, string contentType) UnprotectToBytes(string protectedBase64, string contentType)
		{
			if (string.IsNullOrWhiteSpace(protectedBase64) || string.IsNullOrWhiteSpace(contentType))
				return (false, "Avatar inválido.", Array.Empty<byte>(), string.Empty);

			byte[] protectedBytes;
			try { protectedBytes = Convert.FromBase64String(protectedBase64); }
			catch { return (false, "Avatar inválido.", Array.Empty<byte>(), string.Empty); }

			byte[] bytes;
			try { bytes = _protector.Unprotect(protectedBytes); }
			catch { return (false, "Avatar inválido.", Array.Empty<byte>(), string.Empty); }

			if (bytes.Length == 0 || bytes.Length > MaxBytes)
				return (false, "Avatar inválido.", Array.Empty<byte>(), string.Empty);

			if (!string.Equals(contentType, "image/png", StringComparison.OrdinalIgnoreCase) &&
				!string.Equals(contentType, "image/jpeg", StringComparison.OrdinalIgnoreCase))
				return (false, "Avatar inválido.", Array.Empty<byte>(), string.Empty);

			return (true, string.Empty, bytes, contentType);
		}
	}
}

