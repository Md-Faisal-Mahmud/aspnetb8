using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncapsulateWhatChanges
{
	public class EncryptionUtility
	{
		private readonly IEncryptionKeyReader _encryptionKeyReader;
		public EncryptionUtility(IEncryptionKeyReader encryptionKeyReader)
		{
			_encryptionKeyReader = encryptionKeyReader;
		}

		public string Encrypt(string original)
		{
			Key key = _encryptionKeyReader.GetKey();

			// code to encrypt text
			var encryptedText = string.Empty;

			return encryptedText;
		}
	}
}
