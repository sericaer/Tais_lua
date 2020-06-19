trigger = function()
    return gm_pop().is_consume
            and gm_pop().buffers:is_valid('PINKUN_LEVEL1')
            and gm_pop().consume < 65   
end

occur_days = function ()
    if gm_pop().consume >= 60 then
        return 6 * 30
    end

    if gm_pop().consume >= 50 then
        return 4*30
    end

    return 2*30
end

options = 
{
    OPTION_1 = 
    {
        selected = function()
            gm_pop().buffers:set_valid('PINKUN_LEVEL2')
        end,

        tooltip = function ()
            return {{'POP_BUFFER_VALID', 'PINKUN_LEVEL2'}}
        end
    }
}