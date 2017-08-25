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
    public readonly static Dictionary<Size, Availability> defaultPotionAvailability = new Dictionary<Size, Availability>()
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
    public readonly static Dictionary<Size, Availability> defaultScrollAvailability = new Dictionary<Size, Availability>()
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
    public readonly static Dictionary<Size, Availability> defaultWeaponAvailability = new Dictionary<Size, Availability>()
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
    public readonly static Dictionary<Size, Availability> defaultArmorAvailability = new Dictionary<Size, Availability>()
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
    public readonly static Dictionary<Size, Availability> defaultRingAvailability = new Dictionary<Size, Availability>()
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
    public readonly static Dictionary<Size, Availability> defaultRodAvailability = new Dictionary<Size, Availability>()
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
    public readonly static Dictionary<Size, Availability> defaultStaffAvailability = new Dictionary<Size, Availability>()
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
    public readonly static Dictionary<Size, Availability> defaultWandAvailability = new Dictionary<Size, Availability>()
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
    public readonly static Dictionary<Size, Availability> defaultWondrousAvailability = new Dictionary<Size, Availability>()
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

    public readonly static Dictionary<Size, float> defaultRestockFrequencyModifiers = new Dictionary<Size, float>()
    {
        {Size.Stall, 1.2f },
        {Size.Boutique, 1f },
        {Size.Outlet, 1f },
        {Size.Emporium, 0.8f },
    };

    public string notes;

    public bool sellsWeapons;
    public SpecificWeaponCollection specificWeaponCollection;    // TODO: ADD other specific collections from DevNotes


    public bool sellsArmour;
    public bool sellsScrolls;
    public bool sellsWands;
    public bool sellsPotions;
    public bool sellsStaves;
    public bool sellsRods;
    public bool sellsWondrous;


    public Settlement location;   // TODO: note this should not be in the JSON serialization, just passed to it loading
    public Size size;

    
    public static Shop Create (Settlement settlement, string name, string notes, Size shopSize)
    {
        Shop newShop = CreateInstance<Shop>();
        newShop.location = settlement;
        newShop.name = name;
        newShop.notes = notes;
        newShop.size = shopSize;
        return newShop;
    }


    public static void AddSpecificWeaponCollectionToShop (Shop shop, Availability stockAvailability, WeaponCollection availableWeapons, WeaponQualitiesCollection availableWeaponQualities)
    {
        shop.sellsWeapons = true;
        shop.specificWeaponCollection = SpecificWeaponCollection.Create(stockAvailability, availableWeapons, availableWeaponQualities);
    }


    public static void AddSpecificWeaponCollectionToShop(Shop shop, WeaponCollection availableWeapons, WeaponQualitiesCollection availableWeaponQualities)
    {
        Availability stockAvailability = defaultWeaponAvailability[shop.size];
        AddSpecificWeaponCollectionToShop(shop, stockAvailability, availableWeapons, availableWeaponQualities);
    }


    public void Restock (int daysSinceLastVisit, Dictionary<Size, float> restockFrequencyModifiers)
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
        }        
    }
}
