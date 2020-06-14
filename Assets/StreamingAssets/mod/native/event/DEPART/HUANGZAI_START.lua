trigger = function()
    return gm_depart().buffers:is_invalid('HUANGZAI')
            and gm_depart().crop_grow_percent > 40
end

occur_days = function ()
    return 5*360
end

title = function ()
    return {'HUANGZAI_START_TITLE', gm_depart().name}
end
desc = function ()
    return {'HUANGZAI_START_DESC', gm_depart().name}
end

options = 
{
    OPTION_1 = 
    {
        selected = function ()
            gm_depart().buffers:set_valid('HUANGZAI')
        end,

        tooltip = function ()
            return { {'DEPART_BUFFER_VAILD', gm_depart().name, 'HUANGZAI'} }
        end
    }
}