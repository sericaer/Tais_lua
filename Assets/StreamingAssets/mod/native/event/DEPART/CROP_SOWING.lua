hide = true

trigger = function()
    return gm_data().date.month == 2 and gm_data().date.day == 1
end

options = 
{
    OPTION_1 = 
    {
        selected = function ()
            gm_depart().is_crop_growing = true
        end
    }
}