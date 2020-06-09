EVENT_DEF.DEPART.EVENT_DEPART_HUANGZAI_STOP = 
{
    occur_rate = function ()
        if gm_depart().buffers:find('HUANGZAI').exist == false then
            return 0
        end

        if gm_depart().crop_grow_percent < 20 then
            return 0.1
        end

        if gm_depart().crop_grow_percent < 30 then
            return 0.05
        end

        return 0.01
    end,

    options = 
    {
        OPTION_1 = 
        {
            selected = function ()
                gm_depart().buffers:find('HUANGZAI').exist = false
            end
        }
    }
}