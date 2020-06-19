trigger = function()
    return gm_pop().is_consume
                and gm_pop().buffers:is_valid('PINKUN_LEVEL2')
                and gm_pop().consume >= 70   
end

occur_days = function ()
    if gm_pop().consume >= 80 then
        return 2 * 30
    end
    return 4*30
end

options = 
{
    OPTION_1 = 
    {
        selected = function()
            gm_pop().buffers:set_valid('PINKUN_LEVEL1')
        end,

        tooltip = function ()
            return {{'POP_BUFFER_VALID', 'PINKUN_LEVEL1'}}
        end
    }
}

trigger = 
{
    AND = 
    {
        is.buffer_valid = {gm_pop.buffers, 'PINKUN_LEVEL2' }
        is.true = gm_pop.is_consume
    }
}

occur_days =
{
    base = 4*30,
    modifiy =
    {
        value = -2*60
        is.big_equal = {gm_pop.consume, 80}
    }
}

options = 
{
    OPTION_1 = 
    {
        selected = 
        {
            set.buffer_valid = {gm_pop.buffers, 'PINKUN_LEVEL1' }
        }
    }
}