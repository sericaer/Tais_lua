EVENT_DEF.DEPART.EVENT_DEPART_HUANGZAI_START = 
{
    occur_rate = function ()
        if gm_depart().crop_grow_percent < 40 then
            return 0
        end
        if gm_depart().buffers:is_valid('HUANGZAI') then
            return 0
        end
        return 0.005
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