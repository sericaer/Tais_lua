trigger = function()
    return  gm_depart().buffers:is_valid('YUSHUI')
end

occur_days = function ()
    return 30
end

options = 
{
    OPTION_1 = 
    {
        desc = function()
            return gm_depart().name
        end,

        selected = function ()
            gm_depart().buffers:set_invalid('YUSHUI')
        end
    }
}