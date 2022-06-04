using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using DevExpress.Mvvm;
using Lowadi;
using Lowadi.Models.Shop;
using Lowadi.Models.Type.Shops;
using LowadiBot.Models;

namespace LowadiBot.ViewModels.Windows
{
    public class ItemsInfoNew : ItemsInfo
    {
        public int PurchaseCount { get; set; } = 0;
    }

    internal class ManagerAccountWindowViewModel : ViewModelBase
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand<ItemsInfoNew> BuyCommand { get; set; }
        public ICommand<ItemsInfoNew> SellCommand { get; set; }

        public ObservableCollection<ItemsInfoNew> ItemsInfos { get; private set; }

        public Account Account { get; set; }
        public int Equus { get; set; }

        public ManagerAccountWindowViewModel()
        {
            BuyCommand = new DelegateCommand<ItemsInfoNew>((x) => Buy(x));
            SellCommand = new DelegateCommand<ItemsInfoNew>((x) => Sell(x));
        }

        public ManagerAccountWindowViewModel(Account account = null)
        {
            this.Account = account;
            BuyCommand = new AsyncCommand<ItemsInfoNew>((x) => Buy(x));
            SellCommand = new AsyncCommand<ItemsInfoNew>((x) => Sell(x));
            GetInfo();
        }

        private async void GetInfo()
        {
            IEnumerable<ItemsInfo> info = await Account.LowadiApi.Shop.GetInformation(new List<ItemsType>() {
                ItemsType.Apple,
                ItemsType.Carrot,
                ItemsType.Fertilizer_1,
                ItemsType.Fertilizer_2,
                ItemsType.Flax,
                ItemsType.Forage,
                ItemsType.Iron,
                ItemsType.Leather,
                ItemsType.Manure,
                ItemsType.Oats,
                ItemsType.Sand,
                ItemsType.Straw,
                ItemsType.Wheat,
                ItemsType.Wood,
                ItemsType.CompoundFeed,
                ItemsType.SeedsAlfalfa,
                ItemsType.SeedsApple,
                ItemsType.SeedsCarrot,
                ItemsType.SeedsFlax,
                ItemsType.SeedsOat,
                ItemsType.SeedsPass,
                ItemsType.SeedsWheat
            });
            var data = info.ToList()
                .Select(x => new ItemsInfoNew() { ItemsType = x.ItemsType, Count = x.Count })
                .ToList();

            ItemsInfos = new ObservableCollection<ItemsInfoNew>(data);
            Equus = LowadiApi.Equus;
        }

        private async Task Buy(ItemsInfoNew item)
        {
            if (item.PurchaseCount == 0)
                return;

            var buy = await Account.LowadiApi.Shop.Buy(new ShopData() {
                Id = item.ItemsType, Nombre = item.PurchaseCount
            });
            if (buy.Message == "ok")
                GetInfo();
        }

        private async Task Sell(ItemsInfoNew item)
        {
            if (item.PurchaseCount == 0)
                return;

            var sale = await Account.LowadiApi.Shop.Sale(new ShopData() {
                Id = item.ItemsType, Nombre = item.PurchaseCount
            });
            if (sale.Retour == "ok")
                GetInfo();
        }
    }
}