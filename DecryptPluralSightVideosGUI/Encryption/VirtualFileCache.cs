using System;
using System.IO;
using System.Runtime.InteropServices;

namespace DecryptPluralSightVideosGUI.Encryption
{
    public class VirtualFileCache : IDisposable
    {
        private readonly IPsStream encryptedVideoFile;

        public VirtualFileCache(IPsStream stream)
        {
            this.encryptedVideoFile = stream;
        }

        public VirtualFileCache(string encryptedVideoFilePath)
        {
            this.encryptedVideoFile = new PsStream(encryptedVideoFilePath);
        }

        public void Dispose()
        {
            this.encryptedVideoFile.Dispose();
        }

        public void Read(byte[] pv, int offset, int count, IntPtr pcbRead)
        {
            if (this.Length != 0)
            {
                this.encryptedVideoFile.Seek(offset, SeekOrigin.Begin);
                int length = this.encryptedVideoFile.Read(pv, 0, count);
                VideoEncryption.DecryptBuffer(pv, length, (long)offset);
                if (IntPtr.Zero != pcbRead)
                {
                    Marshal.WriteIntPtr(pcbRead, new IntPtr(length));
                }
            }
        }

        public long Length => this.encryptedVideoFile.Length;
    }
}
