using strange.extensions.command.api;
using strange.extensions.injector.api;
using strange.extensions.mediation.api;
namespace EntrySystem
{
    public class CrossContextBindingConfig
    {
        public CrossContextBindingConfig()
        {
        }
        public void MapBindings(ICrossContextInjectionBinder injectionBinder, ICommandBinder commandBinder,
            IMediationBinder mediationBinder)
        {
            injectionBinder.Bind<OnViewHeroSignal>().ToSingleton();
            
            //
            commandBinder.Bind<ShowPanelHomeSignal>().To<ShowPanelHomeCmd>();
            commandBinder.Bind<ShowPopupStaminaSignal>().To<ShowPopupStaminaCmd>();
            commandBinder.Bind<ShowPanelHeroSignal>().To<ShowPanelHeroCmd>();
            commandBinder.Bind<ShowPanelCraftSignal>().To<ShowPanelCraftCmd>();
            commandBinder.Bind<ShowPanelShopSignal>().To<ShowPanelShopCmd>();
            commandBinder.Bind<ShowPopupGachaSignal>().To<ShowPopupGachaCmd>();
            commandBinder.Bind<EquipGearSignal>().To<EquipGearCmd>();
            commandBinder.Bind<UnequipGearSignal>().To<UnequipGearCmd>();
            commandBinder.Bind<ShowEquipmentDetailSignal>().To<ShowEquipmentDetailCmd>();
            commandBinder.Bind<CraftEquipmentSignal>().To<CraftEquipmentCmd>();
            commandBinder.Bind<ShowPopupCraftSignal>().To<ShowPopupCraftCmd>();
            commandBinder.Bind<ShowPopupGachaInfoSignal>().To<ShowPopupGachaInfoCmd>();
            commandBinder.Bind<ShowRevivePopupSignal>().To<ShowRevivePopupCmd>();
            commandBinder.Bind<ShowRewardGamePlayPopupSignal>().To<ShowRewardGamePlayPopupCmd>();
            commandBinder.Bind<AddRewardFromItemSignal>().To<AddRewardFromItemCmd>();
            commandBinder.Bind<ShowPopupRewardSignal>().To<ShowPopupRewardCmd>();
            commandBinder.Bind<CheckAndConsumeCurrencySignal>().To<CheckAndConsumeCurrencyCmd>();
            
            //RESOURCE
            commandBinder.Bind<AddGoldSignal>().To<AddGoldCmd>();
            commandBinder.Bind<AddGemSignal>().To<AddGemCmd>();
            commandBinder.Bind<AddStaminaSignal>().To<AddStaminaCmd>();
            commandBinder.Bind<AddEquipmentSignal>().To<AddEquipmentCmd>();
            
            //NOTIFICATION
            commandBinder.Bind<NotificationPanelHeroSignal>().To<NotificationPanelHeroCmd>();
            commandBinder.Bind<NotificationPanelCraftSignal>().To<NotificationPanelCraftCmd>();
            //MEDIATOR
            mediationBinder.Bind<InventoryView>().To<InventoryMediator>();
            mediationBinder.Bind<HeroEquipmentView>().To<HeroEquipmentMediator>();
        }
    }
}
