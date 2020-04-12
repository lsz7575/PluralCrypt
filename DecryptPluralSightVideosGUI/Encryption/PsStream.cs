using System.IO;
using File = Pri.LongPath.File;
using FileInfo = Pri.LongPath.FileInfo;

namespace DecryptPluralSightVideosGUI.Encryption
{
    public class PsStream : IPsStream
    {
        private readonly Stream fileStream;
        private long _length;

        public PsStream(string filenamePath)
        {
            this.fileStream = File.Open(filenamePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            this._length = new FileInfo(filenamePath).Length;
        }

        public void Dispose()
        {
            this._length = 0L;
            this.fileStream.Dispose();
        }

        public int Read(byte[] pv, int i, int count)
        {
            return ((this._length > 0L) ? this.fileStream.Read(pv, i, count) : 0);
        }

        public void Seek(int offset, SeekOrigin begin)
        {
            if (this._length > 0L)
            {
                this.fileStream.Seek((long)offset, begin);
            }
        }

        public int BlockSize => 0x40000;

        public long Length => this._length;
    }
}
