using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Falcon.Core.Model;
using Falcon.Core.Services.Interfaces;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using PropertyChanged;

namespace Falcon.GUI.ViewModel
{
    [ImplementPropertyChanged]
    public class MissionLoadoutEditorViewModel : ViewModelBase
    {
        private readonly IVirtualArsenalLoadoutService _arsenalLoadoutService;

        public ObservableCollection<Loadout> MissionLoadouts { get; set; }

        public Loadout CurrentLoadout { get; set; }

        public RelayCommand DoStuffCommand { get; private set; }

        public MissionLoadoutEditorViewModel(IVirtualArsenalLoadoutService arsenalLoadoutService)
        {
            _arsenalLoadoutService = arsenalLoadoutService;

            MissionLoadouts = new ObservableCollection<Loadout>();
            DoStuffCommand = new RelayCommand(DoStuff);
        }

        private void DoStuff()
        {
            MissionLoadouts.Add(new Loadout()
            {
                Name = Guid.NewGuid().ToString(),
                ManEquipment = new ManEquipment
                {
                    Uniform = new Container
                    {
                        Classname = "LOP_U_CDF_Fatigue_01",
                        Items = new List<Item>
                        {
                            new Item {Classname = "ACE_fieldDressing"},
                            new Item {Classname = "ACE_EarPlugs"}
                        }
                    },
                    Vest = new Container
                    {
                        Classname = "LOP_V_6B23_6Sh92_CDF",
                        Items = new List<Item>
                        {
                            new Item {Classname = "ACRE_PRC343_ID_2"},
                            new Item {Classname = "rhs_10Rnd_762x54mmR_7N1"}
                        }
                    },
                    Backpack = new Container
                    {
                        Classname = "rhs_sidor",
                        Items = new List<Item>
                        {
                            new Item {Classname = "ACE_RangeCard"},
                            new Item {Classname = "RH_8Rnd_762_tt33"}
                        }
                    },
                    Headgear = new Item {Classname = "LOP_H_6B27M_CDF"},
                    Goggles = new Item {Classname = "Aviator"},
                    Binocular = new Item {Classname = "Binocular"},
                    PrimaryWeapon = new Weapon
                    {
                        Classname = "rhs_weap_svdp_npz",
                        Attachments = new List<Item>
                        {
                            new Item {Classname = "rhsusf_acc_LEUPOLDMK4"}
                        },
                        Magazine = new Item {Classname = "rhs_10Rnd_762x54mmR_7N1"}
                    },
                    SecondaryWeapon = new Weapon
                    {
                        Classname = "RPG7_v2",
                        Attachments = new List<Item>
                        {
                            new Item {Classname = "PGO"}
                        },
                        Magazine = new Item {Classname = "PG7_VL"}
                    },
                    Sidearm = new Weapon
                    {
                        Classname = "RH_tt33",
                        Attachments = new List<Item>(),
                        Magazine = new Item {Classname = "RH_8Rnd_762_tt33"}
                    },
                    AssignedItems = new List<Item>
                    {
                        new Item {Classname = "ItemMap"},
                        new Item {Classname = "ItemCompass"},
                        new Item {Classname = "ItemWatch"},
                        new Item {Classname = "ItemRadioAcreFlagged"},
                        new Item {Classname = "ItemGPS"}
                    }
                }
            });
        }
    }
}