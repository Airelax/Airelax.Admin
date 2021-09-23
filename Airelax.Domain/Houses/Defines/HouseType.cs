namespace Airelax.Domain.Houses.Defines
{
    public enum HouseType
    {
        // Apartment
        RentalUnit = 101, //出租大樓
        Condo = 102, //私有公寓
        Loft = 103, //loft
        ServicedApartment = 104, //服務式公寓
        CasaParticular = 105, //古巴家庭旅館

        // House
        ResidentialHome = 201, //住宅
        Cabin = 202, //小木屋
        Villa = 203, //別墅
        Townhouse = 204, //連棟房屋
        Cottage = 205, //村舍
        EarthHouse = 206, //生態土屋
        Houseboat = 207, //船屋
        Hut = 208, //小屋
        FarmStay = 209, //農場住宿
        Chalet = 210, //度假小木屋
        Bungalow = 211, //平房

        // SecondaryUnit
        Guesthouse = 301, //客用住房
        GuestSuite = 302, //客用套房
        FarmStayUnit = 303, //農場住宿

        // UniqueSpace
        Barn = 401, //穀倉
        Boat = 402,
        Bus = 403,
        Camper = 404, //露營車
        TreeHouse = 405,
        Campsite = 406, //營地
        Castle = 407,
        Cave = 408, //洞穴
        Igloo = 409, //冰屋
        Island = 410,
        Ranch = 411, //牧場
        ReligiousBuilding = 412, //宗教建築
        ShipperContainer = 413, //貨櫃屋
        Tent = 414, //帳篷
        Train = 415,
        Yurt = 416, //蒙古包
        Other = 417, //其他

        // break and breakfast
        BreakAndBreakfast = 501,
        NatureLodge = 502, //自然小屋
        Farm = 503,
        Minsu = 504, //民宿
        Ryokan = 505, //日式旅館

        // BoutiqueHotel
        Hotel = 601,
        Hostel = 602, //青年旅館
        Resort = 603, //渡假村
        NatureHotel = 604, //自然小屋
        BoutiqueHotel = 605, //精品旅館
        ApartHotel = 606, //公寓式旅店
        ServicedApartmentHotel = 607, //服務式公寓旅館
        HeritageHotel = 608, //文化遺產旅店
        Kezhan = 609 //客棧
    }
}