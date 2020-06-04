EVENT_DEF.DEPART.EVENT_DEPART_HUANGZAI_START = 
{
    occur_rate = function ()
        if gm_depart().crop_growing_percent == 0 then
            return 1
        end
        return 0
    end,

    options = 
    {
        OPTION_1 = 
        {
            desc = function()
                return gm_depart().name
            end,

            selected = function ()
                --gm_depart().buffers:find('HUANGZAI').exist = true
                print(gm_depart().buffers:find('HUANGZAI').name)
            end
        }
    }
}