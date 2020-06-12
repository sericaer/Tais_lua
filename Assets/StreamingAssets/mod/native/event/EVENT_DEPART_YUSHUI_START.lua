EVENT_DEF.DEPART.EVENT_DEPART_YUSHUI_START = 
{
    trigger = function()
        return  (gm_depart().is_crop_growing == true 
                    and gm_depart().buffers.is_valid('YUSHUI') == false)
    end,

    occur_days = function ()
        return 5*360
    end,

    options = 
    {
        OPTION_1 = 
        {
            desc = function()
                return gm_depart().name
            end,

            selected = function ()
                gm_depart().buffers:set_valid('YUSHUI')
            end
        }
    }
}