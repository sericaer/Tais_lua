EVENT_DEF.DEPART.EVENT_CROP_HARVEST = 
{
    hide = false,

    trigger = function()
        return gm_data().date.month == 8 and gm_data().date.day == 15
    end,


    options = 
    {
        OPTION_1 = 
        {
            selected = function ()
                gm_depart().is_crop_growing = false
            end,

            next_event = function()
                if gm_depart().crop_grow_percent < 50 then
                    return 'EVENT_DEPART_HARVEST_NULL'
                end
                if gm_depart().crop_grow_percent < 70 then
                    return 'EVENT_DEPART_HARVEST_LESS'
                end
                if gm_depart().crop_grow_percent >= 95 then
                    return 'EVENT_DEPART_HARVEST_BETTER'
                end
                return 'EVENT_DEPART_HARVEST_NORMAL'
            end
        }
    }
}