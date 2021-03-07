using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BCGL.Services.Navigation
{
    public interface INavigationService
    {
        //Summary:
        //Navigation method to push onto the Navigation Stack
        //parameters: name = TPageModelBase, navigationData, setRoot
        Task NavigateToAsync<TPageModelBase>(object navigationData = null, bool setRoot = false);

        //Summary:
        //Navigation method to pop off of the Navigation Stack
        Task GoBackAsync();
    }
}
