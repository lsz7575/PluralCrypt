using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using STATSTG = System.Runtime.InteropServices.ComTypes.STATSTG;

namespace PluralCrypt.Encryption
{
    internal class VirtualFileStream : IStream, IDisposable
    {
        private long position;
        private readonly object _Lock;
        private VirtualFileCache _Cache;

        private VirtualFileStream(VirtualFileCache Cache)
        {
            this._Lock = new object();
            this._Cache = Cache;
        }

        public VirtualFileStream(string EncryptedVideoFilePath)
        {
            this._Lock = new object();
            this._Cache = new VirtualFileCache(EncryptedVideoFilePath);
        }

        public void Clone(out IStream ppstm)
        {
            ppstm = new VirtualFileStream(this._Cache);
        }

        public void Commit(int grfCommitFlags)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(IStream pstm, long cb, IntPtr pcbRead, IntPtr pcbWritten)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            this._Cache.Dispose();
        }

        public void LockRegion(long libOffset, long cb, int dwLockType)
        {
            throw new NotImplementedException();
        }

        public void Read(byte[] pv, int cb, IntPtr pcbRead)
        {
            if ((this.position < 0L) || (this.position > this._Cache.Length))
            {
                Marshal.WriteIntPtr(pcbRead, new IntPtr(0));
            }
            else
            {
                object obj2 = this._Lock;
                lock (obj2)
                {
                    this._Cache.Read(pv, (int)this.position, cb, pcbRead);
                    this.position += pcbRead.ToInt64();
                }
            }
        }

        public void Revert()
        {
            throw new NotImplementedException();
        }

        public void Seek(long dlibMove, int dwOrigin, IntPtr plibNewPosition)
        {
            SeekOrigin origin = (SeekOrigin)dwOrigin;
            object obj2 = this._Lock;
            lock (obj2)
            {
                switch (origin)
                {
                    case SeekOrigin.Begin:
                        this.position = dlibMove;
                        break;

                    case SeekOrigin.Current:
                        this.position += dlibMove;
                        break;

                    case SeekOrigin.End:
                        this.position = this._Cache.Length + dlibMove;
                        break;
                }
                if (IntPtr.Zero != plibNewPosition)
                {
                    Marshal.WriteInt64(plibNewPosition, this.position);
                }
            }
        }

        public void SetSize(long libNewSize)
        {
            throw new NotImplementedException();
        }

        public void Stat(out STATSTG pstatstg, int grfStatFlag)
        {
            pstatstg = new STATSTG {cbSize = this._Cache.Length};
        }

        public void UnlockRegion(long libOffset, long cb, int dwLockType)
        {
            throw new NotImplementedException();
        }

        public void Write(byte[] pv, int cb, IntPtr pcbWritten)
        {
            throw new NotImplementedException();
        }
    }
}
