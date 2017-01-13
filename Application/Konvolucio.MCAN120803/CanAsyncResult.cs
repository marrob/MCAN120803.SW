
namespace Konvolucio.MCAN120803.API
{

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;


    internal class CanAsyncResult : IAsyncResult, IDisposable
    {
        private object _stateObject;
        private AsyncCallback _userCallback;
        private bool _completed;
        private bool _completedSynchronously;
        private ManualResetEvent _waitEvent;
        private int _messageTransfered;
        private Exception _error;
      
        public CanAsyncResult(AsyncCallback userCallback, object stateObject)
        {
            _stateObject = stateObject;
            _userCallback = userCallback;
            _completedSynchronously = false;
            _completed = false;
            _waitEvent = null;
        }

        public object AsyncState
        {
            get 
            {
                return _stateObject;
            }
        }

        public Exception Error
        {
            get
            {
                lock (this)
                {
                    return _error;
                }
            }
        }
        public int MessageTransfered
        {
            get { return _messageTransfered; }
        }
        public WaitHandle AsyncWaitHandle
        {
            get 
            {
                lock (this)
                {
                    if (_waitEvent == null)
                        _waitEvent = new ManualResetEvent(_completed);
                }
                return _waitEvent;
            }
        }
        
        public bool CompletedSynchronously
        {
            get 
            {
                lock (this)
                {
                    return _completedSynchronously;
                }
            }
        }

        public bool IsCompleted
        {
            get 
            {
                lock (this)
                {
                    return _completed;
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void OnCompletion(bool completedSynchronously, Exception error, int messageTransfered, bool synchronousCallback)
        {
            lock (this)
            {
                _completedSynchronously = completedSynchronously;
                _completed = true;
                _error = error;
                _messageTransfered = messageTransfered;
                if (_waitEvent != null)
                    _waitEvent.Set();
            }
            if (_userCallback != null)
            {
                if (synchronousCallback)
                    RunCallback(null);
                else
                    ThreadPool.QueueUserWorkItem(RunCallback);
            }

        }

        private void RunCallback(object state)
        {
            _userCallback(this);
        }
      
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                lock (this)
                {
                    if (_waitEvent != null)
                        _waitEvent.Close();
                }
            }
        }

    
    }
}

