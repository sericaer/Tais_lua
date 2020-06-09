EVENT_DEF.DEPART.EVENT_DEPART_YUSHUI_STOP = 
{
    occur_rate = function ()
        if gm_depart().buffers:is_valid('YUISHUI') == false then
            return 0
        end
        return 0.03
    end,

    options = 
    {
        OPTION_1 = 
        {
            desc = function()
                return gm_depart().name
            end,

            selected = function ()
                gm_depart().buffers:set_invalid('YUISHUI')
            end
        }
    }
}