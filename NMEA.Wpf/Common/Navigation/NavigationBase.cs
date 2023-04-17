using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Ioc;
using Prism.Regions;

namespace NMEA.Wpf.Common.Navigation
{
    public class NavigationBase : GeneralBase, INavigationAware
    {
        public NavigationBase(IContainerProvider containerProvider) : base(containerProvider)
        {

        }

        // 是否允许被导航
        public virtual bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        // 在页面成为非活动时对该页面执行最后的操作
        public virtual void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }

        // 在第一次打开页面时调用该方法
        public virtual void OnNavigatedTo(NavigationContext navigationContext)
        {

        }
    }
}
