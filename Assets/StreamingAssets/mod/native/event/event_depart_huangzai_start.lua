EVENT_DEF.DEPART.EVENT_DEPART_HUANGZAI_START = 
{
    trigger = function()
        return gm_depart().buffers.is_invalid('HUANGZAI')
                and gm_depart().crop_grow_percent > 40
    end,

    occur_days = function ()
        return 10*360
    end,

    options = 
    {
        OPTION_1 = 
        {
            desc = function()
                return gm_depart().name
            end,

            selected = function ()
                gm_depart().buffers:set_valid('HUANGZAI')
            end
        }
    }
}