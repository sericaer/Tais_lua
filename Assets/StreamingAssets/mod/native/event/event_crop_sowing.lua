EVENT_DEF.DEPART.EVENT_CROP_SOWING = 
{
    hide = true,

    occur_rate = function ()
        if gm_data().date.month == 1 and gm_data().date.day == 10 then
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