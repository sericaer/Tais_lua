EVENT_DEF.DEPART.EVENT_CROP_SOWING = 
{
    hide = true,

    occur_rate = function ()
        if gm_data().date.month == 2 and gm_data().date.day == 1 then
            return 1
        end
        return 0
    end,


    options = 
    {
        OPTION_1 = 
        {
            selected = function ()
                gm_depart().is_crop_growing = true
            end
        }
    }
}