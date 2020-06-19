trigger = function()
    return gm_pop().is_consume
                and gm_pop().buffers:is_valid('PINKUN_LEVEL1')
                and gm_pop().consume >= 80   
end

occur_days = function ()
    if gm_pop().consume >= 90 then
        return 2 * 30
    end

    return 4*30
end

options = 
{
    OPTION_1 = 
    {
        selected = function()
            gm_pop().buffers:set_invalid('PINKUN_LEVEL1')
        end,

        tooltip = function ()
            return {{'POP_BUFFER_INVALID', 'PINKUN_LEVEL1'}}
        end
    }
}