EVENT_DEF.EVENT_CROP_SOWING = 
{
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
                gm_data().is_crop_growing = true
            end
        }
    }
}