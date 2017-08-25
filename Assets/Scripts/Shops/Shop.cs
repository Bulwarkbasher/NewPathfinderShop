using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Shop : ScriptableObject
{
    public enum Size
    {
        Stall,
        Boutique,
        Outlet,
        Emporium,
    }

    // TODO: some of these values cannot be set by ranges from the book.
    // TODO: ranges aren't specified for things like weapons where it can potentially go up to +10 and lead to very bad ranges
    public readonly static Dictionary<Size, Availability> defaultPotionAvailability = new Dictionary<Size, Availability>
    {
        {Size.Stall, new Availability
            {
                stock = new StratRanges
                {
                    minor = new Range { min = 3, max = 6},
                    medium = new Range { min = 0, max = 4},
                    major = new Range { min = 0, max = 0}
                },
                budget = new StratRanges
                {
                    minor = new Range { min = 1000, max = 2000},
                    medium = new Range { min = 2000, max = 3000},
                    major = new Range { min = 3000, max = 4000}
                }
            }
        },
        {Size.Boutique, new Availability
            {
                stock = new StratRanges
                {
                    minor = new Range { min = 4, max = 8},
                    medium = new Range { min = 1, max = 5},
                    major = new Range { min = 0, max = 0}
                },
                budget = new StratRanges
                {
                    minor = new Range { min = 1000, max = 2000},
                    medium = new Range { min = 2000, max = 3000},
                    major = new Range { min = 3000, max = 4000}
                }
            }
        },
        {Size.Outlet, new Availability
            {
                stock = new StratRanges
                {
                    minor = new Range { min = 5, max = 10},
                    medium = new Range { min = 2, max = 6},
                    major = new Range { min = 0, max = 0}
                },
                budget = new StratRanges
                {
                    minor = new Range { min = 1000, max = 2000},
                    medium = new Range { min = 2000, max = 3000},
                    major = new Range { min = 3000, max = 4000}
                }
            }
        },
        {Size.Emporium, new Availability
            {
                stock = new StratRanges
                {
                    minor = new Range { min = 10, max = 20},
                    medium = new Range { min = 5, max = 10},
                    major = new Range { min = 0, max = 0}
                },
                budget = new StratRanges
                {
                    minor = new Range { min = 1000, max = 2000},
                    medium = new Range { min = 2000, max = 3000},
                    major = new Range { min = 3000, max = 4000}
                }
            }
        },
    };
    public readonly static Dictionary<Size, Availability> defaultScrollAvailability = new Dictionary<Size, Availability>
    {
        {Size.Stall, new Availability
            {
                stock = new StratRanges
                {
                    minor = new Range { min = 4, max = 8},
                    medium = new Range { min = 0, max = 2},
                    major = new Range { min = 0, max = 0}
                },
                budget = new StratRanges
                {
                    minor = new Range { min = 1000, max = 2000},
                    medium = new Range { min = 2000, max = 3000},
                    major = new Range { min = 3000, max = 4000}
                }
            }
        },
        {Size.Boutique, new Availability
            {
                stock = new StratRanges
                {
                    minor = new Range { min = 6, max = 12},
                    medium = new Range { min = 1, max = 4},
                    major = new Range { min = 0, max = 0}
                },
                budget = new StratRanges
                {
                    minor = new Range { min = 1000, max = 2000},
                    medium = new Range { min = 2000, max = 3000},
                    major = new Range { min = 3000, max = 4000}
                }
            }
        },
        {Size.Outlet, new Availability
            {
                stock = new StratRanges
                {
                    minor = new Range { min = 9, max = 18},
                    medium = new Range { min = 4, max = 10},
                    major = new Range { min = 1, max = 4}
                },
                budget = new StratRanges
                {
                    minor = new Range { min = 1000, max = 2000},
                    medium = new Range { min = 2000, max = 3000},
                    major = new Range { min = 3000, max = 4000}
                }
            }
        },
        {Size.Emporium, new Availability
            {
                stock = new StratRanges
                {
                    minor = new Range { min = 14, max = 27},
                    medium = new Range { min = 10, max = 22},
                    major = new Range { min = 4, max = 10}
                },
                budget = new StratRanges
                {
                    minor = new Range { min = 1000, max = 2000},
                    medium = new Range { min = 2000, max = 3000},
                    major = new Range { min = 3000, max = 4000}
                }
            }
        },
    };
    public readonly static Dictionary<Size, Availability> defaultWeaponAvailability = new Dictionary<Size, Availability>
    {
        {Size.Stall, new Availability
            {
                stock = new StratRanges
                {
                    minor = new Range { min = 3, max = 6},
                    medium = new Range { min = 1, max = 3},
                    major = new Range { min = 0, max = 0}
                },
                budget = new StratRanges
                {
                    minor = new Range { min = 1000, max = 2000},
                    medium = new Range { min = 2000, max = 3000},
                    major = new Range { min = 3000, max = 4000}
                }
            }
        },
        {Size.Boutique, new Availability
            {
                stock = new StratRanges
                {
                    minor = new Range { min = 5, max = 9},
                    medium = new Range { min = 2, max = 4},
                    major = new Range { min = 0, max = 0}
                },
                budget = new StratRanges
                {
                    minor = new Range { min = 1000, max = 2000},
                    medium = new Range { min = 2000, max = 3000},
                    major = new Range { min = 3000, max = 4000}
                }
            }
        },
        {Size.Outlet, new Availability
            {
                stock = new StratRanges
                {
                    minor = new Range { min = 8, max = 14},
                    medium = new Range { min = 4, max = 7},
                    major = new Range { min = 1, max = 3}
                },
                budget = new StratRanges
                {
                    minor = new Range { min = 1000, max = 2000},
                    medium = new Range { min = 2000, max = 3000},
                    major = new Range { min = 3000, max = 4000}
                }
            }
        },
        {Size.Emporium, new Availability
            {
                stock = new StratRanges
                {
                    minor = new Range { min = 12, max = 21},
                    medium = new Range { min = 6, max = 10},
                    major = new Range { min = 2, max = 5}
                },
                budget = new StratRanges
                {
                    minor = new Range { min = 1000, max = 2000},
                    medium = new Range { min = 2000, max = 3000},
                    major = new Range { min = 3000, max = 4000}
                }
            }
        },
    };
    public readonly static Dictionary<Size, Availability> defaultArmorAvailability = new Dictionary<Size, Availability>
    {
        {Size.Stall, new Availability
            {
                stock = new StratRanges
                {
                    minor = new Range { min = 3, max = 6},
                    medium = new Range { min = 1, max = 3},
                    major = new Range { min = 0, max = 0}
                },
                budget = new StratRanges
                {
                    minor = new Range { min = 1000, max = 2000},
                    medium = new Range { min = 2000, max = 3000},
                    major = new Range { min = 3000, max = 4000}
                }
            }
        },
        {Size.Boutique, new Availability
            {
                stock = new StratRanges
                {
                    minor = new Range { min = 5, max = 9},
                    medium = new Range { min = 2, max = 4},
                    major = new Range { min = 0, max = 0}
                },
                budget = new StratRanges
                {
                    minor = new Range { min = 1000, max = 2000},
                    medium = new Range { min = 2000, max = 3000},
                    major = new Range { min = 3000, max = 4000}
                }
            }
        },
        {Size.Outlet, new Availability
            {
                stock = new StratRanges
                {
                    minor = new Range { min = 8, max = 14},
                    medium = new Range { min = 4, max = 7},
                    major = new Range { min = 1, max = 3}
                },
                budget = new StratRanges
                {
                    minor = new Range { min = 1000, max = 2000},
                    medium = new Range { min = 2000, max = 3000},
                    major = new Range { min = 3000, max = 4000}
                }
            }
        },
        {Size.Emporium, new Availability
            {
                stock = new StratRanges
                {
                    minor = new Range { min = 12, max = 21},
                    medium = new Range { min = 6, max = 10},
                    major = new Range { min = 2, max = 5}
                },
                budget = new StratRanges
                {
                    minor = new Range { min = 1000, max = 2000},
                    medium = new Range { min = 2000, max = 3000},
                    major = new Range { min = 3000, max = 4000}
                }
            }
        },
    };
    public readonly static Dictionary<Size, Availability> defaultRingAvailability = new Dictionary<Size, Availability>
    {
        {Size.Stall, new Availability
            {
                stock = new StratRanges
                {
                    minor = new Range { min = 0, max = 2},
                    medium = new Range { min = 0, max = 0},
                    major = new Range { min = 0, max = 0}
                },
                budget = new StratRanges
                {
                    minor = new Range { min = 1000, max = 2000},
                    medium = new Range { min = 2000, max = 3000},
                    major = new Range { min = 3000, max = 4000}
                }
            }
        },
        {Size.Boutique, new Availability
            {
                stock = new StratRanges
                {
                    minor = new Range { min = 0, max = 3},
                    medium = new Range { min = 0, max = 0},
                    major = new Range { min = 0, max = 0}
                },
                budget = new StratRanges
                {
                    minor = new Range { min = 1000, max = 2000},
                    medium = new Range { min = 2000, max = 3000},
                    major = new Range { min = 3000, max = 4000}
                }
            }
        },
        {Size.Outlet, new Availability
            {
                stock = new StratRanges
                {
                    minor = new Range { min = 1, max = 4},
                    medium = new Range { min = 0, max = 3},
                    major = new Range { min = 0, max = 2}
                },
                budget = new StratRanges
                {
                    minor = new Range { min = 1000, max = 2000},
                    medium = new Range { min = 2000, max = 3000},
                    major = new Range { min = 3000, max = 4000}
                }
            }
        },
        {Size.Emporium, new Availability
            {
                stock = new StratRanges
                {
                    minor = new Range { min = 2, max = 8},
                    medium = new Range { min = 1, max = 4},
                    major = new Range { min = 0, max = 3}
                },
                budget = new StratRanges
                {
                    minor = new Range { min = 1000, max = 2000},
                    medium = new Range { min = 2000, max = 3000},
                    major = new Range { min = 3000, max = 4000}
                }
            }
        },
    };
    public readonly static Dictionary<Size, Availability> defaultRodAvailability = new Dictionary<Size, Availability>
    {
        {Size.Stall, new Availability
            {
                stock = new StratRanges
                {
                    minor = new Range { min = 0, max = 0},
                    medium = new Range { min = 0, max = 0},
                    major = new Range { min = 0, max = 0}
                },
                budget = new StratRanges
                {
                    minor = new Range { min = 1000, max = 2000},
                    medium = new Range { min = 2000, max = 3000},
                    major = new Range { min = 3000, max = 4000}
                }
            }
        },
        {Size.Boutique, new Availability
            {
                stock = new StratRanges
                {
                    minor = new Range { min = 0, max = 0},
                    medium = new Range { min = 0, max = 1},
                    major = new Range { min = 0, max = 0}
                },
                budget = new StratRanges
                {
                    minor = new Range { min = 1000, max = 2000},
                    medium = new Range { min = 2000, max = 3000},
                    major = new Range { min = 3000, max = 4000}
                }
            }
        },
        {Size.Outlet, new Availability
            {
                stock = new StratRanges
                {
                    minor = new Range { min = 0, max = 0},
                    medium = new Range { min = 1, max = 3},
                    major = new Range { min = 0, max = 1}
                },
                budget = new StratRanges
                {
                    minor = new Range { min = 1000, max = 2000},
                    medium = new Range { min = 2000, max = 3000},
                    major = new Range { min = 3000, max = 4000}
                }
            }
        },
        {Size.Emporium, new Availability
            {
                stock = new StratRanges
                {
                    minor = new Range { min = 0, max = 0},
                    medium = new Range { min = 2, max = 5},
                    major = new Range { min = 0, max = 3}
                },
                budget = new StratRanges
                {
                    minor = new Range { min = 1000, max = 2000},
                    medium = new Range { min = 2000, max = 3000},
                    major = new Range { min = 3000, max = 4000}
                }
            }
        },
    };
    public readonly static Dictionary<Size, Availability> defaultStaffAvailability = new Dictionary<Size, Availability>
    {
        {Size.Stall, new Availability
            {
                stock = new StratRanges
                {
                    minor = new Range { min = 0, max = 0},
                    medium = new Range { min = 0, max = 0},
                    major = new Range { min = 0, max = 0}
                },
                budget = new StratRanges
                {
                    minor = new Range { min = 1000, max = 2000},
                    medium = new Range { min = 2000, max = 3000},
                    major = new Range { min = 3000, max = 4000}
                }
            }
        },
        {Size.Boutique, new Availability
            {
                stock = new StratRanges
                {
                    minor = new Range { min = 0, max = 0},
                    medium = new Range { min = 0, max = 1},
                    major = new Range { min = 0, max = 0}
                },
                budget = new StratRanges
                {
                    minor = new Range { min = 1000, max = 2000},
                    medium = new Range { min = 2000, max = 3000},
                    major = new Range { min = 3000, max = 4000}
                }
            }
        },
        {Size.Outlet, new Availability
            {
                stock = new StratRanges
                {
                    minor = new Range { min = 0, max = 0},
                    medium = new Range { min = 1, max = 3},
                    major = new Range { min = 0, max = 1}
                },
                budget = new StratRanges
                {
                    minor = new Range { min = 1000, max = 2000},
                    medium = new Range { min = 2000, max = 3000},
                    major = new Range { min = 3000, max = 4000}
                }
            }
        },
        {Size.Emporium, new Availability
            {
                stock = new StratRanges
                {
                    minor = new Range { min = 0, max = 0},
                    medium = new Range { min = 2, max = 5},
                    major = new Range { min = 0, max = 3}
                },
                budget = new StratRanges
                {
                    minor = new Range { min = 1000, max = 2000},
                    medium = new Range { min = 2000, max = 3000},
                    major = new Range { min = 3000, max = 4000}
                }
            }
        },
    };
    public readonly static Dictionary<Size, Availability> defaultWandAvailability = new Dictionary<Size, Availability>
    {
        {Size.Stall, new Availability
            {
                stock = new StratRanges
                {
                    minor = new Range { min = 1, max = 3},
                    medium = new Range { min = 0, max = 1},
                    major = new Range { min = 0, max = 0}
                },
                budget = new StratRanges
                {
                    minor = new Range { min = 1000, max = 2000},
                    medium = new Range { min = 2000, max = 3000},
                    major = new Range { min = 3000, max = 4000}
                }
            }
        },
        {Size.Boutique, new Availability
            {
                stock = new StratRanges
                {
                    minor = new Range { min = 3, max = 6},
                    medium = new Range { min = 1, max = 3},
                    major = new Range { min = 0, max = 0}
                },
                budget = new StratRanges
                {
                    minor = new Range { min = 1000, max = 2000},
                    medium = new Range { min = 2000, max = 3000},
                    major = new Range { min = 3000, max = 4000}
                }
            }
        },
        {Size.Outlet, new Availability
            {
                stock = new StratRanges
                {
                    minor = new Range { min = 6, max = 10},
                    medium = new Range { min = 3, max = 6},
                    major = new Range { min = 0, max = 0}
                },
                budget = new StratRanges
                {
                    minor = new Range { min = 1000, max = 2000},
                    medium = new Range { min = 2000, max = 3000},
                    major = new Range { min = 3000, max = 4000}
                }
            }
        },
        {Size.Emporium, new Availability
            {
                stock = new StratRanges
                {
                    minor = new Range { min = 10, max = 16},
                    medium = new Range { min = 6, max = 10},
                    major = new Range { min = 0, max = 0}
                },
                budget = new StratRanges
                {
                    minor = new Range { min = 1000, max = 2000},
                    medium = new Range { min = 2000, max = 3000},
                    major = new Range { min = 3000, max = 4000}
                }
            }
        },
    };
    public readonly static Dictionary<Size, Availability> defaultWondrousAvailability = new Dictionary<Size, Availability>
    {
        {Size.Stall, new Availability
            {
                stock = new StratRanges
                {
                    minor = new Range { min = 0, max = 2},
                    medium = new Range { min = 0, max = 0},
                    major = new Range { min = 0, max = 0}
                },
                budget = new StratRanges
                {
                    minor = new Range { min = 1000, max = 2000},
                    medium = new Range { min = 2000, max = 3000},
                    major = new Range { min = 3000, max = 4000}
                }
            }
        },
        {Size.Boutique, new Availability
            {
                stock = new StratRanges
                {
                    minor = new Range { min = 0, max = 3},
                    medium = new Range { min = 0, max = 1},
                    major = new Range { min = 0, max = 0}
                },
                budget = new StratRanges
                {
                    minor = new Range { min = 1000, max = 2000},
                    medium = new Range { min = 2000, max = 3000},
                    major = new Range { min = 3000, max = 4000}
                }
            }
        },
        {Size.Outlet, new Availability
            {
                stock = new StratRanges
                {
                    minor = new Range { min = 0, max = 5},
                    medium = new Range { min = 0, max = 3},
                    major = new Range { min = 0, max = 1}
                },
                budget = new StratRanges
                {
                    minor = new Range { min = 1000, max = 2000},
                    medium = new Range { min = 2000, max = 3000},
                    major = new Range { min = 3000, max = 4000}
                }
            }
        },
        {Size.Emporium, new Availability
            {
                stock = new StratRanges
                {
                    minor = new Range { min = 0, max = 8},
                    medium = new Range { min = 0, max = 5},
                    major = new Range { min = 0, max = 3}
                },
                budget = new StratRanges
                {
                    minor = new Range { min = 1000, max = 2000},
                    medium = new Range { min = 2000, max = 3000},
                    major = new Range { min = 3000, max = 4000}
                }
            }
        },
    };

    public readonly static Dictionary<Size, float> defaultRestockFrequencyModifiers = new Dictionary<Size, float>
    {
        {Size.Stall, 1.2f },
        {Size.Boutique, 1f },
        {Size.Outlet, 1f },
        {Size.Emporium, 0.8f },
    };

    private static readonly string[] k_JsonSplitter =
    {
        "###ShopSplitter###",
    };

    public string notes;
    public Settlement location;   // TODO: note this should not be in the JSON serialization, just passed to it loading
    public Size size;
    public bool sellsWeapons;
    public bool sellsArmour;
    public bool sellsScrolls;
    public bool sellsWands;
    public bool sellsPotions;
    public bool sellsStaves;
    public bool sellsRods;
    public bool sellsWondrous;

    public SpecificWeaponCollection specificWeaponCollection;
    public SpecificArmourCollection specificArmourCollection;
    public SpecificScrollCollection specificScrollCollection;
    public SpecificWandCollection specificWandCollection;
    public SpecificPotionCollection specificPotionCollection;
    public SpecificStaffCollection specificStaffCollection;
    public SpecificRodCollection specificRodCollection;
    public SpecificWondrousCollection specificWondrousCollection;


    public static Shop Create (Settlement settlement, string name, string notes, Size shopSize)
    {
        Shop newShop = CreateInstance<Shop>();
        newShop.location = settlement;
        newShop.name = name;
        newShop.notes = notes;
        newShop.size = shopSize;
        newShop.specificWeaponCollection = CreateInstance<SpecificWeaponCollection> ();
        newShop.specificArmourCollection = CreateInstance<SpecificArmourCollection>();
        newShop.specificScrollCollection = CreateInstance<SpecificScrollCollection>();
        newShop.specificWandCollection = CreateInstance<SpecificWandCollection>();
        newShop.specificPotionCollection = CreateInstance<SpecificPotionCollection>();
        newShop.specificStaffCollection = CreateInstance<SpecificStaffCollection>();
        newShop.specificRodCollection = CreateInstance<SpecificRodCollection>();
        newShop.specificWondrousCollection = CreateInstance<SpecificWondrousCollection>();
        return newShop;
    }

    public static void AddSpecificWeaponCollectionToShop (Shop shop, Availability stockAvailability, WeaponCollection availableWeapons, WeaponQualityCollection availableWeaponQualities)
    {
        shop.sellsWeapons = true;
        shop.specificWeaponCollection = SpecificWeaponCollection.Create(stockAvailability, availableWeapons, availableWeaponQualities);
    }

    public static void AddSpecificWeaponCollectionToShop(Shop shop, WeaponCollection availableWeapons, WeaponQualityCollection availableWeaponQualities)
    {
        Availability stockAvailability = defaultWeaponAvailability[shop.size];
        AddSpecificWeaponCollectionToShop(shop, stockAvailability, availableWeapons, availableWeaponQualities);
    }

    public static void AddSpecificArmourCollectionToShop (Shop shop, Availability stockAvailability, ArmourCollection availableArmours, ArmourQualityCollection availableArmourQualities)
    {
        shop.sellsArmour = true;
        shop.specificArmourCollection = SpecificArmourCollection.Create (stockAvailability, availableArmours, availableArmourQualities);
    }

    public static void AddSpecificArmourCollectionToShop (Shop shop, ArmourCollection availableArmours, ArmourQualityCollection availableArmourQualities)
    {
        Availability stockAvailability = defaultArmorAvailability[shop.size];
        AddSpecificArmourCollectionToShop (shop, stockAvailability, availableArmours, availableArmourQualities);
    }

    public static void AddSpecificScrollArmourCollectionToShop (Shop shop, Availability stockAvailability, SpellCollection availableSpells)
    {
        shop.sellsScrolls = true;
        shop.specificScrollCollection = SpecificScrollCollection.Create (stockAvailability);
    }

    public static void AddSpecificScrollArmourCollectionToShop(Shop shop, SpellCollection availableSpells)
    {
        Availability stockAvailability = defaultScrollAvailability[shop.size];
        shop.specificScrollCollection = SpecificScrollCollection.Create(stockAvailability);
    }

    public static void AddSpecificWandCollectionToShop (Shop shop, Availability stockAvailability, SpellCollection availableSpells)
    {
        shop.sellsWands = true;
        shop.specificWandCollection = SpecificWandCollection.Create (stockAvailability);
    }

    public static void AddSpecificWandCollectionToShop(Shop shop, SpellCollection availableSpells)
    {
        Availability stockAvailability = defaultWandAvailability[shop.size];
        AddSpecificWandCollectionToShop (shop, stockAvailability, availableSpells);
    }

    public static void AddSpecificPotionCollectionToShop(Shop shop, Availability stockAvailability, SpellCollection availableSpells)
    {
        shop.sellsPotions = true;
        shop.specificPotionCollection = SpecificPotionCollection.Create(stockAvailability);
    }

    public static void AddSpecificPotionCollectionToShop(Shop shop, SpellCollection availableSpells)
    {
        Availability stockAvailability = defaultPotionAvailability[shop.size];
        AddSpecificPotionCollectionToShop(shop, stockAvailability, availableSpells);
    }

    public static void AddSpecificStaffCollectionToShop(Shop shop, Availability stockAvailability, SpellCollection availableSpells)
    {
        shop.sellsStaves = true;
        shop.specificStaffCollection = SpecificStaffCollection.Create(stockAvailability);
    }

    public static void AddSpecificStaffCollectionToShop(Shop shop, SpellCollection availableSpells)
    {
        Availability stockAvailability = defaultStaffAvailability[shop.size];
        AddSpecificStaffCollectionToShop(shop, stockAvailability, availableSpells);
    }

    public static void AddSpecificRodCollectionToShop(Shop shop, Availability stockAvailability)
    {
        shop.sellsRods = true;
        shop.specificRodCollection = SpecificRodCollection.Create(stockAvailability);
    }

    public static void AddSpecificRodCollectionToShop(Shop shop)
    {
        Availability stockAvailability = defaultRodAvailability[shop.size];
        AddSpecificRodCollectionToShop(shop, stockAvailability);
    }

    public static void AddSpecificWondrousCollectionToShop(Shop shop, Availability stockAvailability)
    {
        shop.sellsWondrous = true;
        shop.specificWondrousCollection = SpecificWondrousCollection.Create(stockAvailability);
    }

    public static void AddSpecificWondrousCollectionToShop(Shop shop)
    {
        Availability stockAvailability = defaultWondrousAvailability[shop.size];
        AddSpecificWondrousCollectionToShop(shop, stockAvailability);
    }

    public void Restock(int daysSinceLastVisit, Dictionary<Size, float> restockFrequencyModifiers)
    {
        int restockCount = 0;
        int dayCounter = daysSinceLastVisit;

        dayCounter -= Mathf.FloorToInt(location.restockSettings.days.Random() * restockFrequencyModifiers[size]);

        while (dayCounter > 0)
        {
            restockCount++;
            dayCounter -= Mathf.FloorToInt(location.restockSettings.days.Random() * restockFrequencyModifiers[size]);
        }

        for (int i = 0; i < restockCount; i++)
        {
            if (sellsWeapons)
            {
                specificWeaponCollection.Restock(location.restockSettings);
            }
            if (sellsArmour)
            {
                specificArmourCollection.Restock(location.restockSettings);
            }
            if (sellsScrolls)
            {
                specificScrollCollection.Restock(location.restockSettings);
            }
            if (sellsWands)
            {
                specificWandCollection.Restock(location.restockSettings);
            }
            if (sellsPotions)
            {
                specificPotionCollection.Restock(location.restockSettings);
            }
            if (sellsStaves)
            {
                specificStaffCollection.Restock(location.restockSettings);
            }
            if (sellsRods)
            {
                specificRodCollection.Restock(location.restockSettings);
            }
            if (sellsWondrous)
            {
                specificWondrousCollection.Restock(location.restockSettings);
            }
        }        
    }
    // name, notes, size, sellsItem/itemcollection pairs
    public static string GetJsonString (Shop shop)
    {
        string jsonString = "";

        jsonString += shop.name + k_JsonSplitter[0];
        jsonString += shop.notes + k_JsonSplitter[0];
        jsonString += Wrapper<int>.GetJsonString((int)shop.size) + k_JsonSplitter[0];

        jsonString += Wrapper<bool>.GetJsonString(shop.sellsWeapons) + k_JsonSplitter[0];
        jsonString += Wrapper<bool>.GetJsonString(shop.sellsArmour) + k_JsonSplitter[0];
        jsonString += Wrapper<bool>.GetJsonString(shop.sellsScrolls) + k_JsonSplitter[0];
        jsonString += Wrapper<bool>.GetJsonString(shop.sellsWands) + k_JsonSplitter[0];
        jsonString += Wrapper<bool>.GetJsonString(shop.sellsPotions) + k_JsonSplitter[0];
        jsonString += Wrapper<bool>.GetJsonString(shop.sellsStaves) + k_JsonSplitter[0];
        jsonString += Wrapper<bool>.GetJsonString(shop.sellsRods) + k_JsonSplitter[0];
        jsonString += Wrapper<bool>.GetJsonString(shop.sellsWondrous) + k_JsonSplitter[0];

        jsonString += SpecificWeaponCollection.GetJsonString(shop.specificWeaponCollection) + k_JsonSplitter[0];
        jsonString += SpecificArmourCollection.GetJsonString(shop.specificArmourCollection) + k_JsonSplitter[0];
        jsonString += SpecificScrollCollection.GetJsonString(shop.specificScrollCollection) + k_JsonSplitter[0];
        jsonString += SpecificWandCollection.GetJsonString(shop.specificWandCollection) + k_JsonSplitter[0];
        jsonString += SpecificPotionCollection.GetJsonString(shop.specificPotionCollection) + k_JsonSplitter[0];
        jsonString += SpecificStaffCollection.GetJsonString(shop.specificStaffCollection) + k_JsonSplitter[0];
        jsonString += SpecificRodCollection.GetJsonString(shop.specificRodCollection) + k_JsonSplitter[0];
        jsonString += SpecificWondrousCollection.GetJsonString(shop.specificWondrousCollection) + k_JsonSplitter[0];
        
        return jsonString;
    }

    public static Shop CreateFromJsonString (string jsonString, Settlement location)
    {
        string[] splitJsonString = jsonString.Split (k_JsonSplitter, StringSplitOptions.RemoveEmptyEntries);

        Shop shop = CreateInstance<Shop> ();

        shop.name = splitJsonString[0];
        shop.notes = splitJsonString[1];
        shop.size = (Size)Wrapper<int>.CreateFromJsonString (splitJsonString[2]);

        shop.sellsWeapons = Wrapper<bool>.CreateFromJsonString (splitJsonString[3]);
        shop.sellsArmour = Wrapper<bool>.CreateFromJsonString(splitJsonString[4]);
        shop.sellsScrolls = Wrapper<bool>.CreateFromJsonString(splitJsonString[5]);
        shop.sellsWands = Wrapper<bool>.CreateFromJsonString(splitJsonString[6]);
        shop.sellsPotions = Wrapper<bool>.CreateFromJsonString(splitJsonString[7]);
        shop.sellsStaves = Wrapper<bool>.CreateFromJsonString(splitJsonString[8]);
        shop.sellsRods = Wrapper<bool>.CreateFromJsonString(splitJsonString[9]);
        shop.sellsWondrous = Wrapper<bool>.CreateFromJsonString(splitJsonString[10]);

        shop.specificWeaponCollection = SpecificWeaponCollection.CreateFromJsonString (splitJsonString[11]);
        shop.specificArmourCollection = SpecificArmourCollection.CreateFromJsonString (splitJsonString[12]);
        shop.specificScrollCollection = SpecificScrollCollection.CreateFromJsonString (splitJsonString[13]);
        shop.specificWandCollection = SpecificWandCollection.CreateFromJsonString (splitJsonString[14]);
        shop.specificPotionCollection = SpecificPotionCollection.CreateFromJsonString (splitJsonString[15]);
        shop.specificStaffCollection = SpecificStaffCollection.CreateFromJsonString (splitJsonString[16]);
        shop.specificRodCollection = SpecificRodCollection.CreateFromJsonString (splitJsonString[17]);
        shop.specificWondrousCollection = SpecificWondrousCollection.CreateFromJsonString (splitJsonString[18]);

        shop.location = location;

        return shop;
    }
}
