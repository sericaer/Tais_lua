hide = true

trigger = function()
    return gm_data().date.month == 8 and gm_data().date.day == 15
end

title = function ()
    return {'CROP_HARVEST_START_TITLE', gm_depart().name}
end
desc = function ()
    return {'CROP_HARVEST_START_DESC', gm_depart().name}
end

options = 
{
    OPTION_1 = 
    {
        selected = function ()
            gm_depart().is_crop_growing = false
        end,

        next_event = function()
            if gm_depart().crop_grow_percent < 50 then
                return 'CROP_HARVEST_NULL'
            end
            if gm_depart().crop_grow_percent < 70 then
                return 'CROP_HARVEST_LESS'
            end
            if gm_depart().crop_grow_percent >= 90 then
                return 'CROP_HARVEST_BETTER'
            end
            return 'CROP_HARVEST_NORMAL'
        end
    }
}