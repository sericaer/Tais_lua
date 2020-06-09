EVENT_DEF.DEPART.EVENT_DEPART_HARVEST_BETTER = 
{
    options = 
    {
        OPTION_1 = 
        {
            selected = function ()
                gm_depart().buffers:find('HARVEST_BETTER').exist = true
            end,
        }
    }
}