using System;
namespace DsAdmin
{
    public class Factory : IDisposable
    {
        private DAO transacao = new DAO();
        private Boolean disposedValue;

        #region DsAdmin Types

        public Store Store() { return new Store(transacao); }

        #endregion

        protected void Dispose(Boolean disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {
                    if (!transacao.IsClosed) { transacao.Fechar(); }
                }
            }
            this.disposedValue = true;
        }

        public void Dispose()
        {
            if (System.Runtime.InteropServices.Marshal.GetExceptionCode() == 0)
            {
                transacao.Commit();
            }
            else
            {
                transacao.Rollback();
            }

            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }

    public static class Extensions
    {
        public static bool HaveValue(this DateTime? @object)
        {
            return @object.HasValue && @object.Value != DateTime.MinValue;
        }
    }
}