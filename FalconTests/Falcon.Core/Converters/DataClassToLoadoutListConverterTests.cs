using System.Collections.Generic;
using ArmaConfigParser.ConfigModel;
using Falcon.Core.Converters;
using Falcon.Core.Model;
using FluentAssertions;
using NUnit.Framework;

namespace FalconTests.Falcon.Core.Converters
{
    [TestFixture]
    public class DataClassToLoadoutListConverterTests
    {
        private DataClassToLoadoutListConverter _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new DataClassToLoadoutListConverter();
        }

        [Test]
        public void Convert_SingleEmptyLoadout()
        {
            //Arrange
            var input = new DataClass()
            {
                DataType = ConfigDataType.Array,
                Value = new List<ConfigObject>
                {
                    new ItemClass
                    {
                        Data = new DataClass()
                        {
                            DataType = ConfigDataType.String,
                            Value = "EmptyLoadout"
                        }
                    },
                    new ItemClass
                    {
                        Data = new DataClass
                        {
                            DataType = ConfigDataType.Array,
                            Value = new List<ConfigObject>
                            {
                                new ItemClass
                                {
                                    Data = new DataClass
                                    {
                                        DataType = ConfigDataType.Array,
                                        Value = new List<ConfigObject>
                                        {
                                            new ItemClass{ Data = new DataClass { DataType = ConfigDataType.String, Value = ""}},
                                            new ItemClass{ Data = new DataClass { DataType = ConfigDataType.Array, Value = null}}
                                        }
                                    }
                                },
                                new ItemClass
                                {
                                    Data = new DataClass
                                    {
                                        DataType = ConfigDataType.Array,
                                        Value = new List<ConfigObject>
                                        {
                                            new ItemClass{ Data = new DataClass { DataType = ConfigDataType.String, Value = ""}},
                                            new ItemClass{ Data = new DataClass { DataType = ConfigDataType.Array, Value = null}}
                                        }
                                    }
                                },
                                new ItemClass
                                {
                                    Data = new DataClass
                                    {
                                        DataType = ConfigDataType.Array,
                                        Value = new List<ConfigObject>
                                        {
                                            new ItemClass{ Data = new DataClass { DataType = ConfigDataType.String, Value = ""}},
                                            new ItemClass{ Data = new DataClass { DataType = ConfigDataType.Array, Value = null}}
                                        }
                                    }
                                },
                                new ItemClass
                                {
                                    Data = new DataClass
                                    {
                                        DataType = ConfigDataType.String,
                                        Value = ""
                                    }
                                },
                                new ItemClass
                                {
                                Data = new DataClass
                                    {
                                        DataType = ConfigDataType.String,
                                        Value = ""
                                    }
                                },
                                new ItemClass
                                {
                                Data = new DataClass
                                    {
                                        DataType = ConfigDataType.String,
                                        Value = ""
                                    }
                                },
                                new ItemClass
                                {
                                    Data = new DataClass
                                    {
                                        DataType = ConfigDataType.Array,
                                        Value = new List<ConfigObject>
                                        {
                                            new ItemClass{ Data = new DataClass { DataType = ConfigDataType.String, Value = ""}},
                                            new ItemClass{ Data = new DataClass
                                            {
                                                DataType = ConfigDataType.Array,
                                                Value = new List<ConfigObject>
                                                {
                                                    new ItemClass{ Data = new DataClass { DataType = ConfigDataType.String, Value = ""}},
                                                    new ItemClass{ Data = new DataClass { DataType = ConfigDataType.String, Value = ""}},
                                                    new ItemClass{ Data = new DataClass { DataType = ConfigDataType.String, Value = ""}},
                                                    new ItemClass{ Data = new DataClass { DataType = ConfigDataType.String, Value = ""}}
                                                }
                                            }},
                                            new ItemClass{ Data = new DataClass { DataType = ConfigDataType.String, Value = ""}},
                                        }
                                    }
                                },
                                new ItemClass
                                {
                                    Data = new DataClass
                                    {
                                        DataType = ConfigDataType.Array,
                                        Value = new List<ConfigObject>
                                        {
                                            new ItemClass{ Data = new DataClass { DataType = ConfigDataType.String, Value = ""}},
                                            new ItemClass{ Data = new DataClass
                                            {
                                                DataType = ConfigDataType.Array,
                                                Value = new List<ConfigObject>
                                                {
                                                    new ItemClass{ Data = new DataClass { DataType = ConfigDataType.String, Value = ""}},
                                                    new ItemClass{ Data = new DataClass { DataType = ConfigDataType.String, Value = ""}},
                                                    new ItemClass{ Data = new DataClass { DataType = ConfigDataType.String, Value = ""}},
                                                    new ItemClass{ Data = new DataClass { DataType = ConfigDataType.String, Value = ""}}
                                                }
                                            }},
                                            new ItemClass{ Data = new DataClass { DataType = ConfigDataType.String, Value = ""}},
                                        }
                                    }
                                },
                                new ItemClass
                                {
                                    Data = new DataClass
                                    {
                                        DataType = ConfigDataType.Array,
                                        Value = new List<ConfigObject>
                                        {
                                            new ItemClass{ Data = new DataClass { DataType = ConfigDataType.String, Value = ""}},
                                            new ItemClass{ Data = new DataClass
                                            {
                                                DataType = ConfigDataType.Array,
                                                Value = new List<ConfigObject>
                                                {
                                                    new ItemClass{ Data = new DataClass { DataType = ConfigDataType.String, Value = ""}},
                                                    new ItemClass{ Data = new DataClass { DataType = ConfigDataType.String, Value = ""}},
                                                    new ItemClass{ Data = new DataClass { DataType = ConfigDataType.String, Value = ""}},
                                                    new ItemClass{ Data = new DataClass { DataType = ConfigDataType.String, Value = ""}}
                                                }
                                            }},
                                            new ItemClass{ Data = new DataClass { DataType = ConfigDataType.String, Value = ""}},
                                        }
                                    }
                                },
                                new ItemClass
                                {
                                    Data = new DataClass
                                    {
                                        DataType = ConfigDataType.Array,
                                        Value = new List<ConfigObject>
                                        {
                                            
                                        }
                                    }
                                },
                                new ItemClass
                                {
                                    Data = new DataClass
                                    {
                                        DataType = ConfigDataType.Array,
                                        Value = new List<ConfigObject>
                                        {
                                            new ItemClass{ Data = new DataClass { DataType = ConfigDataType.String, Value = ""}},
                                            new ItemClass{ Data = new DataClass { DataType = ConfigDataType.String, Value = ""}},
                                            new ItemClass{ Data = new DataClass { DataType = ConfigDataType.String, Value = ""}}
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            };

            var expectedOutput = new List<Loadout>
            {
                new Loadout()
                {
                    Name = "EmptyLoadout",
                    ManEquipment = new ManEquipment
                    {
                        AssignedItems = new List<Item>()
                    }
                }
            };

            //Act
            var output = _sut.Convert(input);

            //Assert
            output.ShouldBeEquivalentTo(expectedOutput, opt => opt.IncludingAllRuntimeProperties());
        }

        [Test]
        public void Convert_SingleFullLoadout()
        {
            //Arrange
            var input = new DataClass()
            {
                DataType = ConfigDataType.Array,
                Value = new List<ConfigObject>
                {
                    new ItemClass
                    {
                        Data = new DataClass()
                        {
                            DataType = ConfigDataType.String,
                            Value = "Rambo with little ammo"
                        }
                    },
                    new ItemClass
                    {
                        Data = new DataClass
                        {
                            DataType = ConfigDataType.Array,
                            Value = new List<ConfigObject>
                            {
                                new ItemClass //Uniform
                                {
                                    Data = new DataClass
                                    {
                                        DataType = ConfigDataType.Array,
                                        Value = new List<ConfigObject>
                                        {
                                            new ItemClass{ Data = new DataClass { DataType = ConfigDataType.String, Value = "LOP_U_CDF_Fatigue_01"}},
                                            new ItemClass{ Data = new DataClass
                                            {
                                                DataType = ConfigDataType.Array,
                                                Value = new List<ConfigObject>
                                                {
                                                    new ItemClass {Data = new DataClass{ DataType = ConfigDataType.String, Value = "ACE_fieldDressing"}},
                                                    new ItemClass {Data = new DataClass{ DataType = ConfigDataType.String, Value = "ACE_EarPlugs"}},
                                                }
                                            }}
                                        }
                                    }
                                },
                                new ItemClass //Vest
                                {
                                    Data = new DataClass
                                    {
                                        DataType = ConfigDataType.Array,
                                        Value = new List<ConfigObject>
                                        {
                                            new ItemClass{ Data = new DataClass { DataType = ConfigDataType.String, Value = "LOP_V_6B23_6Sh92_CDF"}},
                                            new ItemClass{ Data = new DataClass
                                            {
                                                DataType = ConfigDataType.Array,
                                                Value = new List<ConfigObject>
                                                {
                                                    new ItemClass {Data = new DataClass{ DataType = ConfigDataType.String, Value = "ACRE_PRC343_ID_2"}},
                                                    new ItemClass {Data = new DataClass{ DataType = ConfigDataType.String, Value = "rhs_10Rnd_762x54mmR_7N1"}},
                                                }
                                            }}
                                        }
                                    }
                                },
                                new ItemClass //Backpack
                                {
                                    Data = new DataClass
                                    {
                                        DataType = ConfigDataType.Array,
                                        Value = new List<ConfigObject>
                                        {
                                            new ItemClass{ Data = new DataClass { DataType = ConfigDataType.String, Value = "rhs_sidor"}},
                                            new ItemClass{ Data = new DataClass
                                            {
                                                DataType = ConfigDataType.Array,
                                                Value = new List<ConfigObject>
                                                {
                                                    new ItemClass {Data = new DataClass{ DataType = ConfigDataType.String, Value = "ACE_RangeCard"}},
                                                    new ItemClass {Data = new DataClass{ DataType = ConfigDataType.String, Value = "RH_8Rnd_762_tt33"}},
                                                }
                                            }}
                                        }
                                    }
                                },
                                new ItemClass //Headgear
                                {
                                    Data = new DataClass
                                    {
                                        DataType = ConfigDataType.String,
                                        Value = "LOP_H_6B27M_CDF"
                                    }
                                },
                                new ItemClass //Googles?
                                {
                                Data = new DataClass
                                    {
                                        DataType = ConfigDataType.String,
                                        Value = "Aviator"
                                    }
                                },
                                new ItemClass //Binoculars
                                {
                                Data = new DataClass
                                    {
                                        DataType = ConfigDataType.String,
                                        Value = "Binocular"
                                    }
                                },
                                new ItemClass //Primary weapon
                                {
                                    Data = new DataClass
                                    {
                                        DataType = ConfigDataType.Array,
                                        Value = new List<ConfigObject>
                                        {
                                            new ItemClass{ Data = new DataClass { DataType = ConfigDataType.String, Value = "rhs_weap_svdp_npz"}},
                                            new ItemClass{ Data = new DataClass
                                            {
                                                DataType = ConfigDataType.Array,
                                                Value = new List<ConfigObject>
                                                {
                                                    new ItemClass{ Data = new DataClass { DataType = ConfigDataType.String, Value = ""}},
                                                    new ItemClass{ Data = new DataClass { DataType = ConfigDataType.String, Value = ""}},
                                                    new ItemClass{ Data = new DataClass { DataType = ConfigDataType.String, Value = "rhsusf_acc_LEUPOLDMK4"}},
                                                    new ItemClass{ Data = new DataClass { DataType = ConfigDataType.String, Value = ""}}
                                                }
                                            }},
                                            new ItemClass{ Data = new DataClass { DataType = ConfigDataType.String, Value = "rhs_10Rnd_762x54mmR_7N1"}},
                                        }
                                    }
                                },
                                new ItemClass //Secondary weapon
                                {
                                    Data = new DataClass
                                    {
                                        DataType = ConfigDataType.Array,
                                        Value = new List<ConfigObject>
                                        {
                                            new ItemClass{ Data = new DataClass { DataType = ConfigDataType.String, Value = "RPG7_v2"}},
                                            new ItemClass{ Data = new DataClass
                                            {
                                                DataType = ConfigDataType.Array,
                                                Value = new List<ConfigObject>
                                                {
                                                    new ItemClass{ Data = new DataClass { DataType = ConfigDataType.String, Value = ""}},
                                                    new ItemClass{ Data = new DataClass { DataType = ConfigDataType.String, Value = ""}},
                                                    new ItemClass{ Data = new DataClass { DataType = ConfigDataType.String, Value = "PGO"}},
                                                    new ItemClass{ Data = new DataClass { DataType = ConfigDataType.String, Value = ""}}
                                                }
                                            }},
                                            new ItemClass{ Data = new DataClass { DataType = ConfigDataType.String, Value = "PG7_VL"}},
                                        }
                                    }
                                },
                                new ItemClass //Sidearm
                                {
                                    Data = new DataClass
                                    {
                                        DataType = ConfigDataType.Array,
                                        Value = new List<ConfigObject>
                                        {
                                            new ItemClass{ Data = new DataClass { DataType = ConfigDataType.String, Value = "RH_tt33"}},
                                            new ItemClass{ Data = new DataClass
                                            {
                                                DataType = ConfigDataType.Array,
                                                Value = new List<ConfigObject>
                                                {
                                                    new ItemClass{ Data = new DataClass { DataType = ConfigDataType.String, Value = ""}},
                                                    new ItemClass{ Data = new DataClass { DataType = ConfigDataType.String, Value = ""}},
                                                    new ItemClass{ Data = new DataClass { DataType = ConfigDataType.String, Value = ""}},
                                                    new ItemClass{ Data = new DataClass { DataType = ConfigDataType.String, Value = ""}}
                                                }
                                            }},
                                            new ItemClass{ Data = new DataClass { DataType = ConfigDataType.String, Value = "RH_8Rnd_762_tt33"}},
                                        }
                                    }
                                },
                                new ItemClass //Assigned Items
                                {
                                    Data = new DataClass
                                    {
                                        DataType = ConfigDataType.Array,
                                        Value = new List<ConfigObject>
                                        {
                                            new ItemClass{ Data = new DataClass { DataType = ConfigDataType.String, Value = "ItemMap"}},
                                            new ItemClass{ Data = new DataClass { DataType = ConfigDataType.String, Value = "ItemCompass"}},
                                            new ItemClass{ Data = new DataClass { DataType = ConfigDataType.String, Value = "ItemWatch"}},
                                            new ItemClass{ Data = new DataClass { DataType = ConfigDataType.String, Value = "ItemRadioAcreFlagged"}},
                                            new ItemClass{ Data = new DataClass { DataType = ConfigDataType.String, Value = "ItemGPS"}}
                                        }
                                    }
                                },
                                new ItemClass //Character specific
                                {
                                    Data = new DataClass
                                    {
                                        DataType = ConfigDataType.Array,
                                        Value = new List<ConfigObject>
                                        {
                                            new ItemClass{ Data = new DataClass { DataType = ConfigDataType.String, Value = "WhiteHead_08"}},
                                            new ItemClass{ Data = new DataClass { DataType = ConfigDataType.String, Value = "ACE_NoVoice"}},
                                            new ItemClass{ Data = new DataClass { DataType = ConfigDataType.String, Value = ""}}
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            };

            var expectedOutput = new List<Loadout>
            {
                new Loadout()
                {
                    Name = "Rambo with little ammo",
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
                        Backpack =  new Container
                        {
                            Classname = "rhs_sidor",
                            Items = new List<Item>
                            {
                                new Item {Classname = "ACE_RangeCard"},
                                new Item {Classname = "RH_8Rnd_762_tt33"}
                            }
                        },
                        Headgear = new Item{Classname = "LOP_H_6B27M_CDF"},
                        Goggles = new Item{Classname = "Aviator"},
                        Binocular = new Item{Classname = "Binocular"},
                        PrimaryWeapon = new Weapon
                        {
                            Classname = "rhs_weap_svdp_npz",
                            Attachments = new List<Item>
                            {
                                new Item {Classname = "rhsusf_acc_LEUPOLDMK4"}
                            },
                            Magazine = new Item{Classname = "rhs_10Rnd_762x54mmR_7N1"}
                        },
                        SecondaryWeapon = new Weapon
                        {
                            Classname = "RPG7_v2",
                            Attachments = new List<Item>
                            {
                                new Item {Classname = "PGO"}
                            },
                            Magazine = new Item{Classname = "PG7_VL"}
                        },
                        Sidearm = new Weapon
                        {
                            Classname = "RH_tt33",
                            Attachments = new List<Item>(),
                            Magazine = new Item{Classname = "RH_8Rnd_762_tt33"}
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
                }
            };

            //Act
            var output = _sut.Convert(input);

            //Assert
            output.ShouldBeEquivalentTo(expectedOutput, opt => opt.IncludingAllRuntimeProperties());
        }

        [Test]
        public void Convert_TwoFullLoadouts()
        {
            //Arrange
            var input = new DataClass()
            {
                DataType = ConfigDataType.Array,
                Value = new List<ConfigObject>
                {
                    new ItemClass
                    {
                        Data = new DataClass()
                        {
                            DataType = ConfigDataType.String,
                            Value = "EQ 1"
                        }
                    },
                    new ItemClass
                    {
                        Data = new DataClass
                        {
                            DataType = ConfigDataType.Array,
                            Value = new List<ConfigObject>
                            {
                                new ItemClass //Uniform
                                {
                                    Data = new DataClass
                                    {
                                        DataType = ConfigDataType.Array,
                                        Value = new List<ConfigObject>
                                        {
                                            new ItemClass{ Data = new DataClass { DataType = ConfigDataType.String, Value = "LOP_U_CDF_Fatigue_01"}},
                                            new ItemClass{ Data = new DataClass
                                            {
                                                DataType = ConfigDataType.Array,
                                                Value = new List<ConfigObject>
                                                {
                                                    new ItemClass {Data = new DataClass{ DataType = ConfigDataType.String, Value = "ACE_fieldDressing"}},
                                                    new ItemClass {Data = new DataClass{ DataType = ConfigDataType.String, Value = "ACE_EarPlugs"}},
                                                }
                                            }}
                                        }
                                    }
                                },
                                new ItemClass //Vest
                                {
                                    Data = new DataClass
                                    {
                                        DataType = ConfigDataType.Array,
                                        Value = new List<ConfigObject>
                                        {
                                            new ItemClass{ Data = new DataClass { DataType = ConfigDataType.String, Value = "LOP_V_6B23_6Sh92_CDF"}},
                                            new ItemClass{ Data = new DataClass
                                            {
                                                DataType = ConfigDataType.Array,
                                                Value = new List<ConfigObject>
                                                {
                                                    new ItemClass {Data = new DataClass{ DataType = ConfigDataType.String, Value = "ACRE_PRC343_ID_2"}},
                                                    new ItemClass {Data = new DataClass{ DataType = ConfigDataType.String, Value = "rhs_10Rnd_762x54mmR_7N1"}},
                                                }
                                            }}
                                        }
                                    }
                                },
                                new ItemClass //Backpack
                                {
                                    Data = new DataClass
                                    {
                                        DataType = ConfigDataType.Array,
                                        Value = new List<ConfigObject>
                                        {
                                            new ItemClass{ Data = new DataClass { DataType = ConfigDataType.String, Value = "rhs_sidor"}},
                                            new ItemClass{ Data = new DataClass
                                            {
                                                DataType = ConfigDataType.Array,
                                                Value = new List<ConfigObject>
                                                {
                                                    new ItemClass {Data = new DataClass{ DataType = ConfigDataType.String, Value = "ACE_RangeCard"}},
                                                    new ItemClass {Data = new DataClass{ DataType = ConfigDataType.String, Value = "RH_8Rnd_762_tt33"}},
                                                }
                                            }}
                                        }
                                    }
                                },
                                new ItemClass //Headgear
                                {
                                    Data = new DataClass
                                    {
                                        DataType = ConfigDataType.String,
                                        Value = "LOP_H_6B27M_CDF"
                                    }
                                },
                                new ItemClass //Googles?
                                {
                                Data = new DataClass
                                    {
                                        DataType = ConfigDataType.String,
                                        Value = "Aviator"
                                    }
                                },
                                new ItemClass //Binoculars
                                {
                                Data = new DataClass
                                    {
                                        DataType = ConfigDataType.String,
                                        Value = "Binocular"
                                    }
                                },
                                new ItemClass //Primary weapon
                                {
                                    Data = new DataClass
                                    {
                                        DataType = ConfigDataType.Array,
                                        Value = new List<ConfigObject>
                                        {
                                            new ItemClass{ Data = new DataClass { DataType = ConfigDataType.String, Value = "rhs_weap_svdp_npz"}},
                                            new ItemClass{ Data = new DataClass
                                            {
                                                DataType = ConfigDataType.Array,
                                                Value = new List<ConfigObject>
                                                {
                                                    new ItemClass{ Data = new DataClass { DataType = ConfigDataType.String, Value = ""}},
                                                    new ItemClass{ Data = new DataClass { DataType = ConfigDataType.String, Value = ""}},
                                                    new ItemClass{ Data = new DataClass { DataType = ConfigDataType.String, Value = "rhsusf_acc_LEUPOLDMK4"}},
                                                    new ItemClass{ Data = new DataClass { DataType = ConfigDataType.String, Value = ""}}
                                                }
                                            }},
                                            new ItemClass{ Data = new DataClass { DataType = ConfigDataType.String, Value = "rhs_10Rnd_762x54mmR_7N1"}},
                                        }
                                    }
                                },
                                new ItemClass //Secondary weapon
                                {
                                    Data = new DataClass
                                    {
                                        DataType = ConfigDataType.Array,
                                        Value = new List<ConfigObject>
                                        {
                                            new ItemClass{ Data = new DataClass { DataType = ConfigDataType.String, Value = "RPG7_v2"}},
                                            new ItemClass{ Data = new DataClass
                                            {
                                                DataType = ConfigDataType.Array,
                                                Value = new List<ConfigObject>
                                                {
                                                    new ItemClass{ Data = new DataClass { DataType = ConfigDataType.String, Value = ""}},
                                                    new ItemClass{ Data = new DataClass { DataType = ConfigDataType.String, Value = ""}},
                                                    new ItemClass{ Data = new DataClass { DataType = ConfigDataType.String, Value = "PGO"}},
                                                    new ItemClass{ Data = new DataClass { DataType = ConfigDataType.String, Value = ""}}
                                                }
                                            }},
                                            new ItemClass{ Data = new DataClass { DataType = ConfigDataType.String, Value = "PG7_VL"}},
                                        }
                                    }
                                },
                                new ItemClass //Sidearm
                                {
                                    Data = new DataClass
                                    {
                                        DataType = ConfigDataType.Array,
                                        Value = new List<ConfigObject>
                                        {
                                            new ItemClass{ Data = new DataClass { DataType = ConfigDataType.String, Value = "RH_tt33"}},
                                            new ItemClass{ Data = new DataClass
                                            {
                                                DataType = ConfigDataType.Array,
                                                Value = new List<ConfigObject>
                                                {
                                                    new ItemClass{ Data = new DataClass { DataType = ConfigDataType.String, Value = ""}},
                                                    new ItemClass{ Data = new DataClass { DataType = ConfigDataType.String, Value = ""}},
                                                    new ItemClass{ Data = new DataClass { DataType = ConfigDataType.String, Value = ""}},
                                                    new ItemClass{ Data = new DataClass { DataType = ConfigDataType.String, Value = ""}}
                                                }
                                            }},
                                            new ItemClass{ Data = new DataClass { DataType = ConfigDataType.String, Value = "RH_8Rnd_762_tt33"}},
                                        }
                                    }
                                },
                                new ItemClass //Assigned Items
                                {
                                    Data = new DataClass
                                    {
                                        DataType = ConfigDataType.Array,
                                        Value = new List<ConfigObject>
                                        {
                                            new ItemClass{ Data = new DataClass { DataType = ConfigDataType.String, Value = "ItemMap"}},
                                            new ItemClass{ Data = new DataClass { DataType = ConfigDataType.String, Value = "ItemCompass"}},
                                            new ItemClass{ Data = new DataClass { DataType = ConfigDataType.String, Value = "ItemWatch"}},
                                            new ItemClass{ Data = new DataClass { DataType = ConfigDataType.String, Value = "ItemRadioAcreFlagged"}},
                                            new ItemClass{ Data = new DataClass { DataType = ConfigDataType.String, Value = "ItemGPS"}}
                                        }
                                    }
                                },
                                new ItemClass //Character specific
                                {
                                    Data = new DataClass
                                    {
                                        DataType = ConfigDataType.Array,
                                        Value = new List<ConfigObject>
                                        {
                                            new ItemClass{ Data = new DataClass { DataType = ConfigDataType.String, Value = "WhiteHead_08"}},
                                            new ItemClass{ Data = new DataClass { DataType = ConfigDataType.String, Value = "ACE_NoVoice"}},
                                            new ItemClass{ Data = new DataClass { DataType = ConfigDataType.String, Value = ""}}
                                        }
                                    }
                                }
                            }
                        }
                    },
                    new ItemClass
                    {
                        Data = new DataClass()
                        {
                            DataType = ConfigDataType.String,
                            Value = "EQ 2"
                        }
                    },
                    new ItemClass
                    {
                        Data = new DataClass
                        {
                            DataType = ConfigDataType.Array,
                            Value = new List<ConfigObject>
                            {
                                new ItemClass //Uniform
                                {
                                    Data = new DataClass
                                    {
                                        DataType = ConfigDataType.Array,
                                        Value = new List<ConfigObject>
                                        {
                                            new ItemClass{ Data = new DataClass { DataType = ConfigDataType.String, Value = "LOP_U_CDF_Fatigue_01"}},
                                            new ItemClass{ Data = new DataClass
                                            {
                                                DataType = ConfigDataType.Array,
                                                Value = new List<ConfigObject>
                                                {
                                                    new ItemClass {Data = new DataClass{ DataType = ConfigDataType.String, Value = "ACE_fieldDressing"}},
                                                    new ItemClass {Data = new DataClass{ DataType = ConfigDataType.String, Value = "ACE_EarPlugs"}},
                                                }
                                            }}
                                        }
                                    }
                                },
                                new ItemClass //Vest
                                {
                                    Data = new DataClass
                                    {
                                        DataType = ConfigDataType.Array,
                                        Value = new List<ConfigObject>
                                        {
                                            new ItemClass{ Data = new DataClass { DataType = ConfigDataType.String, Value = "LOP_V_6B23_6Sh92_CDF"}},
                                            new ItemClass{ Data = new DataClass
                                            {
                                                DataType = ConfigDataType.Array,
                                                Value = new List<ConfigObject>
                                                {
                                                    new ItemClass {Data = new DataClass{ DataType = ConfigDataType.String, Value = "ACRE_PRC343_ID_2"}},
                                                    new ItemClass {Data = new DataClass{ DataType = ConfigDataType.String, Value = "rhs_10Rnd_762x54mmR_7N1"}},
                                                }
                                            }}
                                        }
                                    }
                                },
                                new ItemClass //Backpack
                                {
                                    Data = new DataClass
                                    {
                                        DataType = ConfigDataType.Array,
                                        Value = new List<ConfigObject>
                                        {
                                            new ItemClass{ Data = new DataClass { DataType = ConfigDataType.String, Value = "rhs_sidor"}},
                                            new ItemClass{ Data = new DataClass
                                            {
                                                DataType = ConfigDataType.Array,
                                                Value = new List<ConfigObject>
                                                {
                                                    new ItemClass {Data = new DataClass{ DataType = ConfigDataType.String, Value = "ACE_RangeCard"}},
                                                    new ItemClass {Data = new DataClass{ DataType = ConfigDataType.String, Value = "RH_8Rnd_762_tt33"}},
                                                }
                                            }}
                                        }
                                    }
                                },
                                new ItemClass //Headgear
                                {
                                    Data = new DataClass
                                    {
                                        DataType = ConfigDataType.String,
                                        Value = "LOP_H_6B27M_CDF"
                                    }
                                },
                                new ItemClass //Googles?
                                {
                                Data = new DataClass
                                    {
                                        DataType = ConfigDataType.String,
                                        Value = "Aviator"
                                    }
                                },
                                new ItemClass //Binoculars
                                {
                                Data = new DataClass
                                    {
                                        DataType = ConfigDataType.String,
                                        Value = "Binocular"
                                    }
                                },
                                new ItemClass //Primary weapon
                                {
                                    Data = new DataClass
                                    {
                                        DataType = ConfigDataType.Array,
                                        Value = new List<ConfigObject>
                                        {
                                            new ItemClass{ Data = new DataClass { DataType = ConfigDataType.String, Value = "rhs_weap_svdp_npz"}},
                                            new ItemClass{ Data = new DataClass
                                            {
                                                DataType = ConfigDataType.Array,
                                                Value = new List<ConfigObject>
                                                {
                                                    new ItemClass{ Data = new DataClass { DataType = ConfigDataType.String, Value = ""}},
                                                    new ItemClass{ Data = new DataClass { DataType = ConfigDataType.String, Value = ""}},
                                                    new ItemClass{ Data = new DataClass { DataType = ConfigDataType.String, Value = "rhsusf_acc_LEUPOLDMK4"}},
                                                    new ItemClass{ Data = new DataClass { DataType = ConfigDataType.String, Value = ""}}
                                                }
                                            }},
                                            new ItemClass{ Data = new DataClass { DataType = ConfigDataType.String, Value = "rhs_10Rnd_762x54mmR_7N1"}},
                                        }
                                    }
                                },
                                new ItemClass //Secondary weapon
                                {
                                    Data = new DataClass
                                    {
                                        DataType = ConfigDataType.Array,
                                        Value = new List<ConfigObject>
                                        {
                                            new ItemClass{ Data = new DataClass { DataType = ConfigDataType.String, Value = "RPG7_v2"}},
                                            new ItemClass{ Data = new DataClass
                                            {
                                                DataType = ConfigDataType.Array,
                                                Value = new List<ConfigObject>
                                                {
                                                    new ItemClass{ Data = new DataClass { DataType = ConfigDataType.String, Value = ""}},
                                                    new ItemClass{ Data = new DataClass { DataType = ConfigDataType.String, Value = ""}},
                                                    new ItemClass{ Data = new DataClass { DataType = ConfigDataType.String, Value = "PGO"}},
                                                    new ItemClass{ Data = new DataClass { DataType = ConfigDataType.String, Value = ""}}
                                                }
                                            }},
                                            new ItemClass{ Data = new DataClass { DataType = ConfigDataType.String, Value = "PG7_VL"}},
                                        }
                                    }
                                },
                                new ItemClass //Sidearm
                                {
                                    Data = new DataClass
                                    {
                                        DataType = ConfigDataType.Array,
                                        Value = new List<ConfigObject>
                                        {
                                            new ItemClass{ Data = new DataClass { DataType = ConfigDataType.String, Value = "RH_tt33"}},
                                            new ItemClass{ Data = new DataClass
                                            {
                                                DataType = ConfigDataType.Array,
                                                Value = new List<ConfigObject>
                                                {
                                                    new ItemClass{ Data = new DataClass { DataType = ConfigDataType.String, Value = ""}},
                                                    new ItemClass{ Data = new DataClass { DataType = ConfigDataType.String, Value = ""}},
                                                    new ItemClass{ Data = new DataClass { DataType = ConfigDataType.String, Value = ""}},
                                                    new ItemClass{ Data = new DataClass { DataType = ConfigDataType.String, Value = ""}}
                                                }
                                            }},
                                            new ItemClass{ Data = new DataClass { DataType = ConfigDataType.String, Value = "RH_8Rnd_762_tt33"}},
                                        }
                                    }
                                },
                                new ItemClass //Assigned Items
                                {
                                    Data = new DataClass
                                    {
                                        DataType = ConfigDataType.Array,
                                        Value = new List<ConfigObject>
                                        {
                                            new ItemClass{ Data = new DataClass { DataType = ConfigDataType.String, Value = "ItemMap"}},
                                            new ItemClass{ Data = new DataClass { DataType = ConfigDataType.String, Value = "ItemCompass"}},
                                            new ItemClass{ Data = new DataClass { DataType = ConfigDataType.String, Value = "ItemWatch"}},
                                            new ItemClass{ Data = new DataClass { DataType = ConfigDataType.String, Value = "ItemRadioAcreFlagged"}},
                                            new ItemClass{ Data = new DataClass { DataType = ConfigDataType.String, Value = "ItemGPS"}}
                                        }
                                    }
                                },
                                new ItemClass //Character specific
                                {
                                    Data = new DataClass
                                    {
                                        DataType = ConfigDataType.Array,
                                        Value = new List<ConfigObject>
                                        {
                                            new ItemClass{ Data = new DataClass { DataType = ConfigDataType.String, Value = "WhiteHead_08"}},
                                            new ItemClass{ Data = new DataClass { DataType = ConfigDataType.String, Value = "ACE_NoVoice"}},
                                            new ItemClass{ Data = new DataClass { DataType = ConfigDataType.String, Value = ""}}
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            };

            var expectedOutput = new List<Loadout>
            {
                new Loadout()
                {
                    Name = "EQ 1",
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
                        Backpack =  new Container
                        {
                            Classname = "rhs_sidor",
                            Items = new List<Item>
                            {
                                new Item {Classname = "ACE_RangeCard"},
                                new Item {Classname = "RH_8Rnd_762_tt33"}
                            }
                        },
                        Headgear = new Item{Classname = "LOP_H_6B27M_CDF"},
                        Goggles = new Item{Classname = "Aviator"},
                        Binocular = new Item{Classname = "Binocular"},
                        PrimaryWeapon = new Weapon
                        {
                            Classname = "rhs_weap_svdp_npz",
                            Attachments = new List<Item>
                            {
                                new Item {Classname = "rhsusf_acc_LEUPOLDMK4"}
                            },
                            Magazine = new Item{Classname = "rhs_10Rnd_762x54mmR_7N1"}
                        },
                        SecondaryWeapon = new Weapon
                        {
                            Classname = "RPG7_v2",
                            Attachments = new List<Item>
                            {
                                new Item {Classname = "PGO"}
                            },
                            Magazine = new Item{Classname = "PG7_VL"}
                        },
                        Sidearm = new Weapon
                        {
                            Classname = "RH_tt33",
                            Attachments = new List<Item>(),
                            Magazine = new Item{Classname = "RH_8Rnd_762_tt33"}
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
                },
                new Loadout()
                {
                    Name = "EQ 2",
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
                        Backpack =  new Container
                        {
                            Classname = "rhs_sidor",
                            Items = new List<Item>
                            {
                                new Item {Classname = "ACE_RangeCard"},
                                new Item {Classname = "RH_8Rnd_762_tt33"}
                            }
                        },
                        Headgear = new Item{Classname = "LOP_H_6B27M_CDF"},
                        Goggles = new Item{Classname = "Aviator"},
                        Binocular = new Item{Classname = "Binocular"},
                        PrimaryWeapon = new Weapon
                        {
                            Classname = "rhs_weap_svdp_npz",
                            Attachments = new List<Item>
                            {
                                new Item {Classname = "rhsusf_acc_LEUPOLDMK4"}
                            },
                            Magazine = new Item{Classname = "rhs_10Rnd_762x54mmR_7N1"}
                        },
                        SecondaryWeapon = new Weapon
                        {
                            Classname = "RPG7_v2",
                            Attachments = new List<Item>
                            {
                                new Item {Classname = "PGO"}
                            },
                            Magazine = new Item{Classname = "PG7_VL"}
                        },
                        Sidearm = new Weapon
                        {
                            Classname = "RH_tt33",
                            Attachments = new List<Item>(),
                            Magazine = new Item{Classname = "RH_8Rnd_762_tt33"}
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
                },
            };

            //Act
            var output = _sut.Convert(input);

            //Assert
            output.ShouldBeEquivalentTo(expectedOutput, opt => opt.IncludingAllRuntimeProperties());
        }
    }
}