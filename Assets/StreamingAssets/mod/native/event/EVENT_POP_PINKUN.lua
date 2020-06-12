EVENT_DEF.POP.EVENT_POP_PINKUN = 
{
    trigger = function()
        return gm_pop().is_consume == false 
                and gm_pop().buffers:is_valid('PINKUN') ~= true
                and gm_pop().consume < 80
    end,
    
    occur_days = function ()
        if gm_pop().consume >= 70 then
            return 5 * 360
        end

        if gm_pop().consume >= 60 then
            return 2*360
        end

        return 180
    end,

    options = 
    {
        OPTION_1 = 
        {
            selected = function()
                gm_pop().buffers:set_valid('PINKUN')
            end
        }
    }
}