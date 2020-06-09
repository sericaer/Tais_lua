EVENT_DEF.POP.EVENT_POP_PINKUN = 
{
    occur_rate = function ()
        if gm_pop().is_consume == false then
            return 0
        end
        
        if gm_pop().buffers:is_valid('PINKUN') then
            return 0
        end

        if gm_pop().consume >= 80 then
            return 0
        end

        if gm_pop().consume >= 70 then
            return 0.0001
        end

        if gm_pop().consume >= 60 then
            return 0.005
        end

        return 0.01

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