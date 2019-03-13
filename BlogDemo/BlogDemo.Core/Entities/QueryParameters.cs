using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace BlogDemo.Core.Entities
{
    public abstract class QueryParameters :INotifyPropertyChanged
    {
        private const int DefaultPageSize = 10;
        private const int DefaultMaxPageSize = 100;
        /// <summary>
        /// 当前页
        /// </summary>
        private int _pageIndex;
        public virtual int PageIndex
        {   get => _pageIndex;
            set => _pageIndex = value>=0?value:0;
        }

        /// <summary>
        /// 每页大小
        /// </summary>
        private int _pageSize = DefaultPageSize;
        public virtual int PageSize
        {
            get { return _pageSize; }
            set { SetField(ref _pageSize, value); }
        }
        private int _maxPageSize = DefaultMaxPageSize;
        protected internal virtual int MaxPageSize
        {
            get => _maxPageSize;
            set { SetField(ref _maxPageSize, value); }
        }
        /// <summary>
        /// 默认按ID排序
        /// </summary>
        private string _orderBy;
        public string OrderBy
        {
            get { return _orderBy; }
            set { _orderBy = value ?? nameof(EntityBase.Id); }
        }
       
        public string Fields { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
            {
                return false;
            }
            field = value;
            OnPropertyChanged(propertyName);
            if (propertyName == nameof(PageSize) || propertyName == nameof(MaxPageSize))
            {
                SetPageSize();
            }
            return true;
        }

        private void SetPageSize()
        {
            if (_maxPageSize <= 0)
            {
                _maxPageSize = DefaultMaxPageSize;
            }
            if (_pageSize <= 0)
            {
                _pageSize = DefaultPageSize;
            }
            _pageSize = _pageSize > _maxPageSize ? _maxPageSize : _pageSize;
        }
    }
}
