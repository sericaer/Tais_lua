EVENT_DEF.DEPART.EVENT_DEPART_HUANGZAI_STOP = 
{
    trigger = function()
        return gm_depart().buffers.is_valid('HUANGZAI') == true
    end,

    occur_days = function ()
        if gm_depart().crop_grow_percent < 20 then
            return 7
        end

        if gm_depart().crop_grow_percent < 30 then
            return 15
        end

        return 30
    end,

    options = 
    {
        OPTION_1 = 
        {
            selected = function ()
                gm_depart().buffers:set_invalid('HUANGZAI')
            end
        }
    }
}